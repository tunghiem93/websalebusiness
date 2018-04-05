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
                                         Name = pro.Name,
                                         Phone = pro.Phone,
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
                                         CreatedDate = pro.CreatedDate
                                     }).ToList();

                    var _images = cxt.dbImage.Select(x => new
                    {
                        ID = x.ID,
                        ProductID = x.ProductID,
                        ImageUrL = x.ImageUrL
                    }).ToList();

                    lstResult.ForEach(x =>
                    {
                        if(_images != null && _images.Any())
                        {
                            var defaultImage = _images.Where(z=>z.ProductID.Equals(x.ID)).FirstOrDefault();
                            if(defaultImage != null)
                            {
                                x.ImageURL = defaultImage.ImageUrL;
                                /// add list image
                                var _temp = _images.Where(z => z.ProductID.Equals(x.ID) && z.ID != defaultImage.ID)
                                                     .Select(m => new ImageProduct
                                                     {
                                                         ImageURL = m.ImageUrL,
                                                         IsDelete = true
                                                     }).ToList();
                                var _offSet = 0;
                                _temp.ForEach(k =>
                                {
                                    k.OffSet = _offSet;
                                    _offSet++;
                                });
                                if (_temp != null && _temp.Any())
                                {
                                    x.ListImg.AddRange(_temp);
                                }
                            }

                        }
                    });
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
                                      Name = pro.Name,
                                      Phone = pro.Phone,
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

                    var _images = cxt.dbImage.Select(x => new
                    {
                        ID = x.ID,
                        ProductID = x.ProductID,
                        ImageUrL = x.ImageUrL
                    }).Where(z=>z.ProductID == id).ToList();

                    if (_images != null && _images.Any())
                    {
                        var defaultImage = _images[0];
                        result.ImageURL = defaultImage.ImageUrL;
                        /// add list image
                        var _temp = _images.Where(z => z.ID != defaultImage.ID)
                                             .Select(m => new ImageProduct
                                             {
                                                 ImageURL = m.ImageUrL,
                                                 IsDelete = true
                                             }).ToList();
                        var _offSet = 0;
                        _temp.ForEach(k =>
                        {
                            k.OffSet = _offSet;
                            _offSet++;
                        });
                        if (_temp != null && _temp.Any())
                        {
                            result.ListImg.AddRange(_temp);
                        }
                    }
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
                        item.Name = model.Name;
                        item.Phone = model.Phone;
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
                        cxt.dbProduct.Add(item);

                        //////////////////////////////////// Save table Image
                        var lstEImage = new List<Image>();
                        model.ListImageUrl.ForEach(x =>
                        {
                            lstEImage.Add(new Image
                            {
                                ID = Guid.NewGuid().ToString(),
                                ImageUrL = x,
                                ProductID = id
                            });
                        });
                        cxt.dbImage.AddRange(lstEImage);
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
                using (var tran = cxt.Database.BeginTransaction())
                {
                    try
                    {
                        var _image = cxt.dbImage.Where(x => x.ProductID == id).ToList();
                        if (_image != null)
                            cxt.dbImage.RemoveRange(_image);

                        var item = cxt.dbProduct.Where(x => x.ID == id).FirstOrDefault();
                        if (item != null)
                        {
                            cxt.dbProduct.Remove(item);
                            cxt.SaveChanges();
                            tran.Commit();
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
                        tran.Rollback();
                        result = false;
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
                            itemUpdate.Name = model.Name;
                            itemUpdate.Phone = model.Phone;
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
                            
                            ///// update image
                            if (model.ListImageUrl != null && model.ListImageUrl.Count > 0)
                            {
                                var images = cxt.dbImage.Where(x => x.ProductID == model.ID).ToList();
                                if (images != null)
                                    cxt.dbImage.RemoveRange(images);


                                var lstEImage = new List<Image>();
                                model.ListImageUrl.ForEach(x =>
                                {
                                    lstEImage.Add(new Image
                                    {
                                        ID = Guid.NewGuid().ToString(),
                                        ImageUrL = x,
                                        ProductID = model.ID
                                    });
                                });
                                cxt.dbImage.AddRange(lstEImage);
                            }                            

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
        public List<string> GetListImageProduct(string id)
        {
            using (DataContext cxt = new DataContext())
            {
                List<string> result = new List<string>();
                try
                {
                    var _lstImages = cxt.dbImage.Where(w=>w.ProductID == id).Select(x => new
                    {
                        ImageUrL = x.ImageUrL
                    }).ToList();
                    _lstImages.ForEach(o =>
                    {
                        if (!string.IsNullOrEmpty(o.ImageUrL))
                        {
                            result.Add(o.ImageUrL);
                        }
                    });
                    return result;
                }
                catch (Exception ex)
                {
                    NSLog.Logger.Error("GetListImagesProduct_Fail", ex);
                    return null;
                }
            }
        }
    }
}
