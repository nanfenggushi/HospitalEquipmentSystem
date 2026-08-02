using System;
using System.Collections.Generic;
using System.Data; // 改用 System.Data 以支持更通用的 IDataReader
using System.Reflection;

namespace HospitalEquipmentSystem.Common
{
    /// <summary>
    /// 数据读取器映射工具类
    /// </summary>
    public static class DataReaderMapper
    {
        /// <summary>
        /// 将 IDataReader 中的数据批量映射转换为指定类型的对象列表
        /// </summary>
        /// <typeparam name="T">目标实体类型（必须包含无参构造函数）</typeparam>
        /// <param name="reader">实现了 IDataReader 接口的数据读取器</param>
        /// <returns>映射后的实体列表</returns>
        public static List<T> MapToList<T>(IDataReader reader) where T : new()
        {
            var list = new List<T>();
            if (reader == null || reader.IsClosed) return list;

            // 1. 获取目标类型的所有公开可写的实例属性
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 2. 预先建立数据库列名与列索引(Ordinal)的字典，避免在循环中重复查询列名（大幅提升性能）
            var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                columnMap[reader.GetName(i)] = i;
            }

            // 3. 预先计算出“属性-列索引”的有效映射关系，只保留能映射上的属性
            var validMappings = new List<(PropertyInfo Property, int Ordinal)>();
            foreach (var prop in properties)
            {
                // 排除不可写的属性
                if (!prop.CanWrite) continue;

                // 如果数据库结果集中包含该属性同名的列，则保存映射
                if (columnMap.TryGetValue(prop.Name, out int ordinal))
                {
                    validMappings.Add((prop, ordinal));
                }
            }

            // 4. 遍历数据行进行数据填充
            while (reader.Read())
            {
                T item = new T();
                foreach (var (property, ordinal) in validMappings)
                {
                    // 检查数据库字段是否为 DBNull
                    if (!reader.IsDBNull(ordinal))
                    {
                        object value = reader.GetValue(ordinal);

                        // 转换类型以兼容 Nullable<T>、枚举和相近的数据类型（如 int -> long, decimal -> double）
                        object safeValue = ChangeType(value, property.PropertyType);

                        property.SetValue(item, safeValue);
                    }
                }
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        /// 安全的类型转换辅助方法（处理 Nullable、Enum 及基础类型转换）
        /// </summary>
        private static object ChangeType(object value, Type targetType)
        {
            if (value == null || value == DBNull.Value) return null;

            // 处理可空类型 (Nullable<T>)，获取其底层实际类型 (例如 int? -> int)
            Type underlyingType = Nullable.GetUnderlyingType(targetType) ?? targetType;

            // 处理枚举类型
            if (underlyingType.IsEnum)
            {
                return Enum.ToObject(underlyingType, value);
            }

            // 处理基础数据类型转换 (如数据库返回 long，实体属性为 int)
            return Convert.ChangeType(value, underlyingType);
        }
    }
}