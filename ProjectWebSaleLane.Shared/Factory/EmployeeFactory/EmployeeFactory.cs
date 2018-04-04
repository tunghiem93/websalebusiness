using ProjectWebSaleLand.Data;
using ProjectWebSaleLand.Data.Entities;
using ProjectWebSaleLand.Shared.Model.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLand.Shared.Factory.EmployeeFactory
{
    public class EmployeeFactory
    {
        public List<EmployeeModels> GetListEmployee()
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var lstResult = cxt.dbEmployee.Select(o => new EmployeeModels() {
                        ID = o.ID,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        Name = o.Name,
                        Email = o.Email,
                        Password = o.Password,
                        ZipCode = o.ZipCode,
                        Company = o.Company,
                        WebSite = o.WebSite,
                        Phone = o.Phone,
                        BirthDate = o.BirthDate,
                        Gender = o.Gender,
                        IsSupperAdmin = o.IsSupperAdmin,
                        IsActive = o.IsActive,
                        CreatedDate = o.CreatedDate,
                        CreatedUser = o.CreatedUser,
                        ModifiedDate = o.ModifiedDate,
                        ModifiedUser = o.ModifiedUser,
                        ImageURL = o.ImageURL,
                        Description = o.Description,
                        Address = o.Address
                    }).ToList();
                    return lstResult;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetListEmployee_Fail", ex);
                    return null;
                }
            }
        }

        public EmployeeModels GetDetailEmployee(string id)
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var result = cxt.dbEmployee.Where(w => w.ID == id).Select(o => new EmployeeModels()
                    {
                        ID = o.ID,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        Name = o.Name,
                        Email = o.Email,
                        Password = o.Password,
                        ZipCode = o.ZipCode,
                        Company = o.Company,
                        WebSite = o.WebSite,
                        Phone = o.Phone,
                        BirthDate = o.BirthDate,
                        Gender = o.Gender,
                        IsSupperAdmin = o.IsSupperAdmin,
                        IsActive = o.IsActive,
                        CreatedDate = o.CreatedDate,
                        CreatedUser = o.CreatedUser,
                        ModifiedDate = o.ModifiedDate,
                        ModifiedUser = o.ModifiedUser,
                        ImageURL = o.ImageURL,
                        Description= o.Description,
                        Address = o.Address
                    }).FirstOrDefault();
                    return result;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetDetailEmployee_Fail", ex);
                    return null;
                }
            }
        }

        public bool InsertEmployee(EmployeeModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        Employee item = new Employee();
                        string id = Guid.NewGuid().ToString();
                        item.ID = id;
                        item.FirstName = model.FirstName;
                        item.LastName = model.LastName;
                        item.Name = model.Name;
                        item.Phone = model.Phone;
                        item.ZipCode = model.ZipCode;
                        item.Company = model.Company;
                        item.WebSite = model.WebSite;
                        item.BirthDate = model.BirthDate;
                        item.Gender = model.Gender;
                        item.IsSupperAdmin = model.IsSupperAdmin;
                        item.IsActive = model.IsActive;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedUser = model.CreatedUser;
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedUser = model.CreatedUser;
                        item.ImageURL = model.ImageURL;
                        item.Address = model.Address;
                        item.Description = model.Description;
                        cxt.dbEmployee.Add(item);
                        cxt.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể thêm nhân viên. Làm ơn kiểm tra lại!", ex);
                        result = false;
                        transaction.Rollback();
                    }
                    finally
                    {
                        if (cxt != null)
                            cxt.Dispose();
                    }
                }
            }
            return result;
        }

        public bool DeleteEmployee(string id, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var item = cxt.dbEmployee.Where(x => x.ID == id).FirstOrDefault();
                    if (item != null)
                    {
                        cxt.dbEmployee.Remove(item);
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Nhân viên này hiện đang trong hợp đồng. Làm ơn kiểm tra lại!";
                        result = false;
                    }
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error(msg, ex);
                    result = false;
                }
                finally
                {
                    if (cxt != null)
                        cxt.Dispose();
                }
            }
            return result;
        }

        public bool UpdateEmployee(EmployeeModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var itemUpdate = cxt.dbEmployee.Where(x => x.ID == model.ID).FirstOrDefault();
                        if (itemUpdate != null)
                        {
                            itemUpdate.FirstName = model.FirstName;
                            itemUpdate.LastName = model.LastName;
                            itemUpdate.Name = model.Name;
                            itemUpdate.Email = model.Email;
                            itemUpdate.Password = model.Password;
                            itemUpdate.ZipCode = model.ZipCode;
                            itemUpdate.Company = model.Company;
                            itemUpdate.WebSite = model.WebSite;
                            itemUpdate.Phone = model.Phone;
                            itemUpdate.BirthDate = model.BirthDate;
                            itemUpdate.Gender = model.Gender;
                            itemUpdate.IsSupperAdmin = model.IsSupperAdmin;
                            itemUpdate.IsActive = model.IsActive;
                            itemUpdate.ModifiedDate = DateTime.Now;
                            itemUpdate.ModifiedUser = model.CreatedUser;
                            itemUpdate.ImageURL = model.ImageURL;
                            itemUpdate.Description = model.Description;
                            itemUpdate.Address = model.Address;
                            cxt.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể cập nhập cho nhân viên này. Làm ơn kiểm tra lại!", ex);
                        result = false;
                        transaction.Rollback();
                    }
                    finally
                    {
                        if (cxt != null)
                            cxt.Dispose();
                    }
                }
            }
            return result;
        }
    }
}
