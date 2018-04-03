using ProjectWebSaleLand.Data;
using ProjectWebSaleLand.Data.Entities;
using ProjectWebSaleLand.Shared.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLand.Shared.Factory.ProductFactory
{
    public class ProductFactory
    {
        public List<ProductModels> GetListProduct()
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var lstResult = (from pro in cxt.dbProduct
                                     from loc in cxt.dbLocation
                                     where (pro.LocationID == loc.ID)
                                     orderby pro.CreatedDate descending
                                     select new ProductModels()
                                     {
                                         ID = pro.ID,
                                         Address = pro.Address,
                                         Length = pro.Length,
                                         Width = pro.Width,
                                         Acreage = pro.Acreage,
                                         Price = pro.Price,
                                         Location = loc.Name,
                                         Right = pro.Right,
                                         BedRoom = pro.BedRoom,
                                         LivingRoom = pro.LivingRoom,
                                         BathRoom = pro.BathRoom,
                                         Floor = pro.Floor,
                                         Description = pro.Description,
                                         Phone1 = pro.Phone1,
                                         Address1 = pro.Address1,
                                         Phone2 = pro.Phone2,
                                         Address2 = pro.Address2,
                                         LocationID = loc.ID,
                                         Type = pro.Type
                                     }).ToList();
                    return lstResult;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetListProduct_Fail", ex);
                    return null;
                }
            }
        }

        public ProductModels GetDetailProduct(string id)
        {
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var result = (from pro in cxt.dbProduct
                                  from loc in cxt.dbLocation
                                  where (pro.ID == id && pro.LocationID == loc.ID)
                                  orderby pro.CreatedDate descending
                                  select new ProductModels()
                                  {
                                      ID = pro.ID,
                                      Address = pro.Address,
                                      Length = pro.Length,
                                      Width = pro.Width,
                                      Acreage = pro.Acreage,
                                      Price = pro.Price,
                                      Location = loc.Name,
                                      Right = pro.Right,
                                      BedRoom = pro.BedRoom,
                                      LivingRoom = pro.LivingRoom,
                                      BathRoom = pro.BathRoom,
                                      Floor = pro.Floor,
                                      Description = pro.Description,
                                      Phone1 = pro.Phone1,
                                      Address1 = pro.Address1,
                                      Phone2 = pro.Phone2,
                                      Address2 = pro.Address2,
                                      LocationID = loc.ID,
                                      Type = pro.Type,
                                      IsActive = pro.IsActive
                                  }).FirstOrDefault();
                    return result;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetListProduct_Fail", ex);
                    return null;
                }
            }
        }

        public bool InsertProduct(ProductModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        Product item = new Product();
                        string id = Guid.NewGuid().ToString();
                        item.ID = id;
                        item.Address = model.Address;
                        item.Length = model.Length;
                        item.Width = model.Width;
                        item.Acreage = model.Acreage;
                        item.Price = model.Price;
                        item.LocationID = model.LocationID;
                        item.Type = model.Type;
                        item.Right = model.Right;
                        item.BedRoom = model.BedRoom;
                        item.LivingRoom = model.LivingRoom;
                        item.BathRoom = model.BathRoom;
                        item.Floor = model.Floor;
                        item.Description = model.Description;
                        item.Phone1 = model.Phone1;
                        item.Address1 = model.Address1;
                        item.Phone2 = model.Phone2;
                        item.Address2 = model.Address2;
                        item.IsActive = model.IsActive;
                        item.CreatedDate = DateTime.Now;
                        item.ModifiedDate = DateTime.Now;
                        item.CreatedUser = model.CreatedUser;
                        item.ModifiedUser = model.ModifiedUser;
                        item.Type = model.Type;
                        cxt.dbProduct.Add(item);
                        cxt.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể thêm sản phẩm!", ex);
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

        public bool DeleteProduct(string id, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                try
                {
                    var item = cxt.dbProduct.Where(x => x.ID == id).FirstOrDefault();
                    if (item != null)
                    {
                        cxt.dbProduct.Remove(item);
                        cxt.SaveChanges();
                    }
                    else
                    {
                        msg = "Sản phẩm này hiện đang sử dụng. Làm ơn kiểm tra lại!";
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

        public bool UpdateProduct(ProductModels model, ref string msg)
        {
            bool result = true;
            using (DataContext cxt = new DataContext())
            {
                using (var transaction = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var itemUpdate = cxt.dbProduct.Where(x => x.ID == model.ID).FirstOrDefault();
                        if (itemUpdate != null)
                        {
                            //itemUpdate.ID = model.ID;
                            itemUpdate.Address = model.Address;
                            itemUpdate.Length = model.Length;
                            itemUpdate.Width = model.Width;
                            itemUpdate.Acreage = model.Acreage;
                            itemUpdate.Price = model.Price;
                            itemUpdate.LocationID = model.LocationID;
                            itemUpdate.Type = model.Type;
                            itemUpdate.Right = model.Right;
                            itemUpdate.BedRoom = model.BedRoom;
                            itemUpdate.LivingRoom = model.LivingRoom;
                            itemUpdate.BathRoom = model.BathRoom;
                            itemUpdate.Floor = model.Floor;
                            itemUpdate.Description = model.Description;
                            itemUpdate.Phone1 = model.Phone1;
                            itemUpdate.Address1 = model.Address1;
                            itemUpdate.Phone2 = model.Phone2;
                            itemUpdate.Address2 = model.Address2;
                            itemUpdate.IsActive = model.IsActive;
                            cxt.SaveChanges();
                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        NSLog.Logger.Error("Không thể cập nhập cho sản phẩm này. Làm ơn kiểm tra lại!", ex);
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
