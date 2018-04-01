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
                        Name = o.Name,
                        Email = o.Email,
                        Password = o.Password,
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
                        Name = o.Name,
                        Email = o.Email,
                        Password = o.Password,
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
                        item.Name = model.Name;
                        item.Email = model.Email;
                        item.Password = model.Password;
                        item.Phone = model.Phone;
                        item.BirthDate = model.BirthDate;
                        item.Gender = model.Gender;
                        item.IsSupperAdmin = model.IsSupperAdmin;
                        item.IsActive = model.IsActive;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedUser = model.CreatedUser;
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedUser = model.CreatedUser;
                        item.ImageURL = model.ImageURL;

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
                            itemUpdate.Name = itemUpdate.Name;
                            itemUpdate.Email = itemUpdate.Email;
                            itemUpdate.Password = itemUpdate.Password;
                            itemUpdate.Phone = itemUpdate.Phone;
                            itemUpdate.BirthDate = itemUpdate.BirthDate;
                            itemUpdate.Gender = itemUpdate.Gender;
                            itemUpdate.IsSupperAdmin = itemUpdate.IsSupperAdmin;
                            itemUpdate.IsActive = itemUpdate.IsActive;
                            itemUpdate.ModifiedDate = DateTime.Now;
                            itemUpdate.ModifiedUser = itemUpdate.CreatedUser;
                            itemUpdate.ImageURL = itemUpdate.ImageURL;
                            
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
