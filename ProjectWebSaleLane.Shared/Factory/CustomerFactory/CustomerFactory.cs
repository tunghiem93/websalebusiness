using ProjectWebSaleLand.Data;
using ProjectWebSaleLand.Data.Entities;
using ProjectWebSaleLand.Shared.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLand.Shared.Factory.CustomerFactory
{
    public class CustomerFactory
    {
        public List<CustomerModels> GetListCustomer()
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var lstResult = cxt.dbEmployee.Select(o => new CustomerModels()
                    {
                        ID = o.ID,
                        Name = o.Name,
                        Email = o.Email,
                        Password = o.Password,
                        Phone = o.Phone,
                        BirthDate = o.BirthDate,
                        Gender = o.Gender,
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
                    NSLog.Logger.Error("GetListCustomer_Fail", ex);
                    return null;
                }
            }
        }

        public CustomerModels GetDetailCustomer(string id)
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var result = cxt.dbCustomer.Where(w => w.ID == id).Select(o => new CustomerModels()
                    {
                        ID = o.ID,
                        Name = o.Name,
                        Email = o.Email,
                        Password = o.Password,
                        Phone = o.Phone,
                        BirthDate = o.BirthDate,
                        Gender = o.Gender,
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
                    NSLog.Logger.Error("GetDetailCustomer_Fail", ex);
                    return null;
                }
            }
        }

        public bool InsertCustomer(CustomerModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        Customer item = new Customer();
                        string id = Guid.NewGuid().ToString();
                        item.ID = id;
                        item.Name = model.Name;
                        item.Email = model.Email;
                        item.Password = model.Password;
                        item.Phone = model.Phone;
                        item.BirthDate = model.BirthDate;
                        item.Gender = model.Gender;
                        item.IsActive = model.IsActive;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedUser = model.CreatedUser;
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedUser = model.CreatedUser;
                        item.ImageURL = model.ImageURL;

                        cxt.dbCustomer.Add(item);
                        cxt.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể thêm khách hàng. Làm ơn kiểm tra lại!", ex);
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

        public bool DeleteCustomer(string id, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var item = cxt.dbCustomer.Where(x => x.ID == id).FirstOrDefault();
                    if (item != null)
                    {
                        cxt.dbCustomer.Remove(item);
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Khách hàng này hiện vẫn đang hoạt động. Làm ơn kiểm tra lại!";
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

        public bool UpdateCustomer(CustomerModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var itemUpdate = cxt.dbCustomer.Where(x => x.ID == model.ID).FirstOrDefault();
                        if (itemUpdate != null)
                        {
                            itemUpdate.ID = itemUpdate.ID;
                            itemUpdate.Name = itemUpdate.Name;
                            itemUpdate.Email = itemUpdate.Email;
                            itemUpdate.Password = itemUpdate.Password;
                            itemUpdate.Phone = itemUpdate.Phone;
                            itemUpdate.BirthDate = itemUpdate.BirthDate;
                            itemUpdate.Gender = itemUpdate.Gender;
                            itemUpdate.IsActive = itemUpdate.IsActive;
                            itemUpdate.CreatedDate = itemUpdate.CreatedDate;
                            itemUpdate.CreatedUser = itemUpdate.CreatedUser;
                            itemUpdate.ModifiedDate = DateTime.Now;
                            itemUpdate.ModifiedUser = itemUpdate.ModifiedUser;
                            itemUpdate.ImageURL = itemUpdate.ImageURL;

                            cxt.dbCustomer.Add(itemUpdate);
                            cxt.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể cập nhập cho khách hàng này. Làm ơn kiểm tra lại!", ex);
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
