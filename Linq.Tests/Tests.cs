using System;
using System.Collections.Generic;
using System.Linq;
using Linq.Objects;
using NUnit.Framework;
using Linq.Tests.Comparers;

namespace Linq.Tests
{
    [TestFixture]
    public class Tests
    {
        private readonly YearSchoolComparer _yearSchoolComparer = new YearSchoolComparer();
        private readonly NumberPairComparer _numberPairComparer = new NumberPairComparer();
        private readonly MaxDiscountOwnerComparer _discountOwnerComparer = new MaxDiscountOwnerComparer();
        private readonly CountryStatComparer _countryStatComparer = new CountryStatComparer();

       

        [Test]
        public void Task14Test()
        {
            foreach (var (supplierList, supplierDiscountList, expected) in Task2Data())
            {
                var actualResult = Tasks.Task2(supplierList, supplierDiscountList);
                AssertIsLinq(actualResult);
                AssertIsAsExpected(expected, actualResult, _discountOwnerComparer);
            }
        }
        
        private IEnumerable<(IEnumerable<Supplier> supplierList, IEnumerable<SupplierDiscount> supplierDiscountList,
        IEnumerable<MaxDiscountOwner> expected)> Task2Data()
        {
            yield return (
                    supplierList: new []
                    {
                        new Supplier{Adress = "adress 1", Id = 1, YearOfBirth = 2000},
                        new Supplier{Adress = "adress 2", Id = 2, YearOfBirth = 1961},
                        new Supplier{Adress = "adress 3", Id = 3, YearOfBirth = 1984},
                        new Supplier{Adress = "adress 4", Id = 4, YearOfBirth = 1997},
                        new Supplier{Adress = "adress 5", Id = 5, YearOfBirth = 1990}
                    },
                    supplierDiscountList: new []
                    {
                        new SupplierDiscount{Discount = 5.0, ShopName = "shop1", SupplierId = 1},
                        new SupplierDiscount{Discount = 1.0, ShopName = "shop2", SupplierId = 1},
                        new SupplierDiscount{Discount = 2.0, ShopName = "shop3", SupplierId = 1},
                        new SupplierDiscount{Discount = 4.0, ShopName = "shop4", SupplierId = 1},
                        new SupplierDiscount{Discount = 6.0, ShopName = "shop6", SupplierId = 1},
                        new SupplierDiscount{Discount = 34.0, ShopName = "shop2", SupplierId = 2},
                        new SupplierDiscount{Discount = 10.0, ShopName = "shop5", SupplierId = 2},
                        new SupplierDiscount{Discount = 7.0, ShopName = "shop5", SupplierId = 3},
                        new SupplierDiscount{Discount = 9.0, ShopName = "shop3", SupplierId = 3},
                        new SupplierDiscount{Discount = 32.0, ShopName = "shop5", SupplierId = 3},
                        new SupplierDiscount{Discount = 7.2, ShopName = "shop4", SupplierId = 4},
                        new SupplierDiscount{Discount = 34.0, ShopName = "shop2", SupplierId = 4},
                        new SupplierDiscount{Discount = 6.0, ShopName = "shop3", SupplierId = 4},
                        new SupplierDiscount{Discount = 7.0, ShopName = "shop5", SupplierId = 4},
                        new SupplierDiscount{Discount = 1.5, ShopName = "shop6", SupplierId = 5}
                    },
                    expected: new []
                    {
                        new MaxDiscountOwner
                        {
                            Discount = 5.0, ShopName = "shop1",
                            Owner = new Supplier{Adress = "adress 1", Id = 1, YearOfBirth = 2000}
                        },
                        new MaxDiscountOwner
                        {
                            Discount = 34.0, ShopName = "shop2",
                            Owner = new Supplier{Adress = "adress 2", Id = 2, YearOfBirth = 1961}
                        },
                        new MaxDiscountOwner
                        {
                            Discount = 9.0, ShopName = "shop3",
                            Owner = new Supplier{Adress = "adress 3", Id = 3, YearOfBirth = 1984}
                        },
                        new MaxDiscountOwner
                        {
                            Discount = 7.2, ShopName = "shop4",
                            Owner = new Supplier{Adress = "adress 4", Id = 4, YearOfBirth = 1997}
                        },
                        new MaxDiscountOwner
                        {
                            Discount = 32.0, ShopName = "shop5",
                            Owner = new Supplier{Adress = "adress 3", Id = 3, YearOfBirth = 1984}
                        },
                        new MaxDiscountOwner
                        {
                            Discount = 6.0, ShopName = "shop6",
                            Owner = new Supplier{Adress = "adress 1", Id = 1, YearOfBirth = 2000}
                        }
                    });
        }

        #region Utility

        private void AssertIsLinq<T>(IEnumerable<T> result)
        {
            Assert.AreEqual("System.Linq", result.GetType().Namespace, "Result is not linq");
        }

        private void AssertIsAsExpected<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            Assert.AreEqual(expected, actual);
        }

        private void AssertIsAsExpected<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> comparer)
        {
            Assert.True(expected.SequenceEqual(actual, comparer), "Result is not as expected");
        }

        #endregion
    }
}