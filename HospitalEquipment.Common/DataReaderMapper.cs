using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace HospitalEquipment.Util {
    public static class DataReaderMapper {
        public static List<T> MapToList<T>(SqlDataReader reader) where T : new() {
            var list = new List<T>();
            var properties = typeof(T).GetProperties();

            while (reader.Read()) {
                T item = new T();
                foreach (var prop in properties) {
                    if (HasColumn(reader, prop.Name) && !reader.IsDBNull(reader.GetOrdinal(prop.Name))) {
                        prop.SetValue(item, reader[prop.Name]);
                    }
                }
                list.Add(item);
            }
            return list;
        }

        private static bool HasColumn(SqlDataReader reader, string columnName) {
            for (int i = 0; i < reader.FieldCount; i++) {
                if (reader.GetName(i).Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}
