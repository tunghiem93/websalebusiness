﻿using ProjectWebSaleLand.Data;
using ProjectWebSaleLand.Data.Entities;
using ProjectWebSaleLand.Shared.Model.Customer;
using ProjectWebSaleLane.Shared.Model.Customer;
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
                    var lstResult = cxt.dbCustomer.Select(o => new CustomerModels()
                    {
                        ID = o.ID,
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        Email = o.Email,
                        Password = o.Password,
                        ZipCode = o.ZipCode,
                        Company = o.Company,
                        WebSite = o.WebSite,
                        Phone = o.Phone,
                        BirthDate = o.BirthDate,
                        Gender = o.Gender,
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
                        FirstName = o.FirstName,
                        LastName = o.LastName,
                        Email = o.Email,
                        Password = o.Password,
                        ZipCode = o.ZipCode,
                        Company = o.Company,
                        WebSite = o.WebSite,
                        Phone = o.Phone,
                        BirthDate = o.BirthDate,
                        Gender = o.Gender,
                        IsActive = o.IsActive,
                        CreatedDate = o.CreatedDate,
                        CreatedUser = o.CreatedUser,
                        ModifiedDate = o.ModifiedDate,
                        ModifiedUser = o.ModifiedUser,
                        ImageURL = o.ImageURL,
                        Description = o.Description,
                        Address = o.Address
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

        public bool InsertCustomer(CustomerModels model, ref string msg, ref string cusId)
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
                        item.FirstName = model.FirstName;
                        item.LastName = model.LastName;
                        item.Name = model.Name;
                        item.Email = model.Email;
                        item.Password = model.Password;
                        item.ZipCode = model.ZipCode;
                        item.Company = model.Company;
                        item.WebSite = model.WebSite;
                        item.Phone = model.Phone;
                        item.BirthDate = model.BirthDate;
                        item.Gender = model.Gender;
                        item.IsActive = model.IsActive;
                        item.CreatedDate = DateTime.Now;
                        item.CreatedUser = model.CreatedUser;
                        item.ModifiedDate = DateTime.Now;
                        item.ModifiedUser = model.CreatedUser;
                        item.ImageURL = model.ImageURL;
                        item.Address = model.Address;
                        item.Description = model.Description;

                        cxt.dbCustomer.Add(item);
                        cxt.SaveChanges();
                        transaction.Commit();
                        cusId = id;
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

        public ClientLoginModel Login(ClientLoginModel model)
        {
            try
            {
                using (var cxt = new DataContext())
                {
                    var data = cxt.dbCustomer.Where(x => x.Email.Equals(model.Email) 
                                                        && x.Password.Equals(model.Password)
                                                        && x.IsActive)
                                              .Select(x=> new ClientLoginModel
                                              {
                                                  Email = x.Email,
                                                  DisplayName = x.Name,
                                                  Password = x.Password
                                              })
                                              .FirstOrDefault();
                    return data;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Login", ex);
            }
            return null;
        }
    }
}
