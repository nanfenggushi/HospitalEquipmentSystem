using HospitalEquipment.Model;
using HospitalEquipmentSystem.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HospitalEquipment.DAL
{
    public class SupplierDAL
    {
        public async Task<List<Supplier>> GetAll()
        {
            string sql= @"SELECT SupplierId, SupplierName, 
                                  ISNULL(SupplierCode, '') AS SupplierCode,
                                  ContactPerson, Phone, 
                                  ISNULL(Email, '') AS Email,
                                  ISNULL(Address, '') AS Address,
                                  ISNULL(Website, '') AS Website,
                                  ISNULL(Remark, '') AS Remark,
                                  IsActive, CreatedAt
                           FROM Suppliers ORDER BY SupplierName";
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                if (reader == null) return new List<Supplier>();
                return DataReaderMapper.MapToList<Supplier>(reader);
            }
        }
        /// <summary>
        /// 获取所有启用的供应商（用于下拉框）
        /// </summary>
        /// <returns></returns>
        public async Task<List<Supplier>> GetActive()
        {
            string sql = @"SELECT SupplierId, SupplierName, 
                                  ISNULL(SupplierCode, '') AS SupplierCode,
                                  ContactPerson, Phone, 
                                  ISNULL(Email, '') AS Email,
                                  ISNULL(Address, '') AS Address,
                                  ISNULL(Website, '') AS Website,
                                  ISNULL(Remark, '') AS Remark,
                                  IsActive, CreatedAt
                           FROM Suppliers WHERE IsActive=1 ORDER BY SupplierName";
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql).ConfigureAwait(false))
            {
                if (reader == null) return new List<Supplier>();
                return DataReaderMapper.MapToList<Supplier>(reader);
            }
        }
        /// <summary>
        /// 根据ID获取供应商详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Supplier> GetById(int id)
        {
            string sql = @"SELECT SupplierId, SupplierName, 
                                  ISNULL(SupplierCode, '') AS SupplierCode,
                                  ContactPerson, Phone, 
                                  ISNULL(Email, '') AS Email,
                                  ISNULL(Address, '') AS Address,
                                  ISNULL(Website, '') AS Website,
                                  ISNULL(Remark, '') AS Remark,
                                  IsActive, CreatedAt
                           FROM Suppliers WHERE SupplierId = @Id";
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, new SqlParameter("@Id", id)).ConfigureAwait(false))
            {
                List<Supplier> list = DataReaderMapper.MapToList<Supplier>(reader);
                return list.Count > 0 ? list[0] : null;
            }
        }
        /// <summary>
        /// 新增供应商（使用 ?? "" 将 NULL 转为空字符串）
        /// </summary>
        public async Task<int> Insert(Supplier supplier)
        {
            string sql = @"INSERT INTO Suppliers 
                           (SupplierName, SupplierCode, ContactPerson, Phone, Email, Address, Website, Remark, IsActive)
                           VALUES (@Name, @Code, @Contact, @Phone, @Email, @Address, @Website, @Remark, @IsActive)";
            SqlParameter[] parameters = {
                new SqlParameter("@Name", supplier.SupplierName),
                new SqlParameter("@Code", supplier.SupplierCode ?? ""),
                new SqlParameter("@Contact", supplier.ContactPerson ?? ""),
                new SqlParameter("@Phone", supplier.Phone ?? ""),
                new SqlParameter("@Email", supplier.Email ?? ""),
                new SqlParameter("@Address", supplier.Address ?? ""),
                new SqlParameter("@Website", supplier.Website ?? ""),
                new SqlParameter("@Remark", supplier.Remark ?? ""),
                new SqlParameter("@IsActive", supplier.IsActive)
            };
            return await DbHelper.ExecuteNonQueryAsync(sql, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 更新供应商（使用 ?? "" 将 NULL 转为空字符串）
        /// </summary>
        public async Task<int> Update(Supplier supplier)
        {
            string sql = @"UPDATE Suppliers SET 
                           SupplierName = @Name,
                           SupplierCode = @Code,
                           ContactPerson = @Contact,
                           Phone = @Phone,
                           Email = @Email,
                           Address = @Address,
                           Website = @Website,
                           Remark = @Remark,
                           IsActive = @IsActive
                           WHERE SupplierId = @Id";
            SqlParameter[] parameters = {
                new SqlParameter("@Id", supplier.SupplierId),
                new SqlParameter("@Name", supplier.SupplierName),
                new SqlParameter("@Code", supplier.SupplierCode ?? ""),
                new SqlParameter("@Contact", supplier.ContactPerson ?? ""),
                new SqlParameter("@Phone", supplier.Phone ?? ""),
                new SqlParameter("@Email", supplier.Email ?? ""),
                new SqlParameter("@Address", supplier.Address ?? ""),
                new SqlParameter("@Website", supplier.Website ?? ""),
                new SqlParameter("@Remark", supplier.Remark ?? ""),
                new SqlParameter("@IsActive", supplier.IsActive)
            };
            return await DbHelper.ExecuteNonQueryAsync(sql, parameters).ConfigureAwait(false);
        }

        /// <summary>
        /// 删除供应商（软删除）
        /// </summary>
        public async Task<int> Delete(int id)
        {
            string sql = "UPDATE Suppliers SET IsActive = 0 WHERE SupplierId = @Id";
            return await DbHelper.ExecuteNonQueryAsync(sql, new SqlParameter("@Id", id)).ConfigureAwait(false);
        }

        /// <summary>
        /// 检查供应商是否被设备引用
        /// </summary>
        public async Task<int> GetEquipmentCount(int supplierId)
        {
            string sql = "SELECT COUNT(*) FROM Equipment WHERE SupplierId = @SupplierId";
            object result = await DbHelper.ExecuteScalarAsync(sql, new SqlParameter("@SupplierId", supplierId)).ConfigureAwait(false);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        /// <summary>
        /// 检查供应商名称是否重复
        /// </summary>
        public async Task<bool> IsNameExists(string name, int excludeId = 0)
        {
            string sql = "SELECT COUNT(*) FROM Suppliers WHERE SupplierName = @Name AND SupplierId != @Id";
            object result = await DbHelper.ExecuteScalarAsync(sql,
                new SqlParameter("@Name", name),
                new SqlParameter("@Id", excludeId)).ConfigureAwait(false);
            return result != null && Convert.ToInt32(result) > 0;
        }

        /// <summary>
        /// 搜索供应商
        /// </summary>
        public async Task<List<Supplier>> Search(string keyword)
        {
            string sql = @"SELECT SupplierId, SupplierName, 
                                  ISNULL(SupplierCode, '') AS SupplierCode,
                                  ContactPerson, Phone, 
                                  ISNULL(Email, '') AS Email,
                                  ISNULL(Address, '') AS Address,
                                  ISNULL(Website, '') AS Website,
                                  ISNULL(Remark, '') AS Remark,
                                  IsActive, CreatedAt
                           FROM Suppliers 
                           WHERE SupplierName LIKE @Keyword OR SupplierCode LIKE @Keyword OR ContactPerson LIKE @Keyword
                           ORDER BY SupplierName";
            using (SqlDataReader reader = await DbHelper.ExecuteReaderAsync(sql, new SqlParameter("@Keyword", $"%{keyword}%")).ConfigureAwait(false))
            {
                if (reader == null) return new List<Supplier>();
                return DataReaderMapper.MapToList<Supplier>(reader);
            }
        }
    

    }
}
