using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Domain;

namespace Dao.Mapping {
    public class ProductMap : ClassMapping<Product>{
        public ProductMap() {
            Lazy(true);
            Id(x => x.Id, m => { m.Column("ProductId"); m.Generator(Generators.Native); });
            Property(x => x.ProductName);
            Property(x => x.ImageName);
            Property(x => x.ImageExtension);
            Property(x => x.AvailableQuantity);
            Property(x => x.PaymentCode);
            Property(x => x.PublishedDate);
            Property(x => x.Priority);
            Property(x => x.Enable);
            ManyToOne(x => x.CategoryItem, map => {

                                                map.Class(typeof(CategoryItem));
                                                map.Column("CategoryItemId");
                                            });
            Bag(x => x.Photos, collectionMap => {
                                    collectionMap.Access(Accessor.NoSetter);
                                    collectionMap.Cascade(Cascade.All);
                                    collectionMap.Key(x => x.Column("ProductId"));
                                    collectionMap.Lazy(CollectionLazy.Lazy);
                                }, m => m.OneToMany());
        }
    }// class
}
