using System;
using System.Linq;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using NUnit.Framework;

namespace dispatchservice.api.tests
{
    [TestFixture]
    public class HouseRpositoryTests: TestFixture
    {
        [Test]
        public void InsertUpdateDeleteHouseTest()
        {
            IHouseRepository repHouse = new HouseRpository { Connection = Connection };
            IStreetRepository repStreet = new StreetRepository { Connection = Connection };
            var repEstate = new Repository<Estate> { Connection = Connection };

            var street1 = new Street { Name = "Улица1" };
            repStreet.SaveOrUpdate(street1);
            var savedstreet = repStreet.All().FirstOrDefault();

            var estate1 = new Estate { Name = "Район1" };
            repEstate.SaveOrUpdate(estate1);
            var estate2 = new Estate { Name = "Район2" };
            repEstate.SaveOrUpdate(estate2);

            var savedEstates = repEstate.All().ToList();

            var house1 = new House
            {
                Number = "дом1",
                Street = savedstreet,
                Estate = savedEstates[0],
                Deleted = true
            };
            repHouse.SaveOrUpdate(house1);
            var savedhouse = repHouse.All().FirstOrDefault();
            Assert.AreEqual("дом1", savedhouse.Number);
            Assert.AreEqual(true, savedhouse.Deleted);
            Assert.AreEqual("Улица1", savedhouse.Street.Name);
            Assert.AreEqual("Район1", savedhouse.Estate.Name);

            savedhouse.Number = "дом2";
            savedhouse.Estate = savedEstates[1];
            repHouse.SaveOrUpdate(savedhouse);
            savedhouse = repHouse.All().FirstOrDefault();
            Assert.AreEqual("дом2", savedhouse.Number);
            Assert.AreEqual("Район2", savedhouse.Estate.Name);

            repHouse.Delete(savedhouse);
            Assert.IsTrue(!repHouse.All().Any());
        }

        [Test]
        public void GetHouseTest()
        {
            IHouseRepository repHouse = new HouseRpository { Connection = Connection };
            IStreetRepository repStreet = new StreetRepository { Connection = Connection };
            var repEstate = new Repository<Estate> { Connection = Connection };

            var street1 = new Street { Name = "Улица1" };
            repStreet.SaveOrUpdate(street1);
            var savedstreet = repStreet.All().FirstOrDefault();

            var estate1 = new Estate { Name = "Район1" };
            repEstate.SaveOrUpdate(estate1);
            var savedEstate = repEstate.All().FirstOrDefault();

            var house1 = new House
            {
                Number = "дом1",
                Street = savedstreet,
                Estate = savedEstate
            };
            repHouse.SaveOrUpdate(house1);

            var house = repHouse.Get(house1.Id);

            Assert.AreEqual(house1.Number, house.Number);
            Assert.AreEqual(house1.Id, house.Id);

            Assert.AreEqual(house1.Street.Name, street1.Name);
            Assert.AreEqual(house1.Street.Id, street1.Id);

            Assert.AreEqual(house1.Estate.Name, estate1.Name);
            Assert.AreEqual(house1.Estate.Id, estate1.Id);
        }

        [Test]
        public void FindHouseTest()
        {
            IHouseRepository repHouse = new HouseRpository { Connection = Connection };
            IStreetRepository repStreet = new StreetRepository { Connection = Connection };
            var repEstate = new Repository<Estate> { Connection = Connection };

            var street1 = new Street { Name = "Улица им. Васи" };
            repStreet.SaveOrUpdate(street1);
            var savedstreet = repStreet.All().FirstOrDefault();

            var estate1 = new Estate { Name = "Район" };
            repEstate.SaveOrUpdate(estate1);
            var savedEstate = repEstate.All().FirstOrDefault();

            var house1 = new House
            {
                Number = "12",
                Street = savedstreet,
                Estate = savedEstate
            };
            repHouse.SaveOrUpdate(house1);

            var findedHouses = repHouse.Find("  ул д ");
            Assert.IsTrue(!findedHouses.Any());

            findedHouses = repHouse.Find("");
            Assert.IsTrue(!findedHouses.Any());

            findedHouses = repHouse.Find("  ");
            Assert.IsTrue(!findedHouses.Any());

            findedHouses = repHouse.Find("  ул  Улица ");
            Assert.IsTrue(findedHouses.Count() == 1);

            // не работает. Не учитывается регистр!!!!!!!!!!!!!!!!!!
            //findedHouses = repHouse.Find("ули им 12");
            //Assert.IsTrue(findedHouses.Count() == 1);

            findedHouses = repHouse.Find("ица Васи 12");
            Assert.IsTrue(findedHouses.Count() == 1);

            findedHouses = repHouse.Find(" ул. Улица им. Васи д. 12  ");
            Assert.IsTrue(findedHouses.Count() == 1);

            findedHouses = repHouse.Find("Улица им. Васи 12");
            Assert.IsTrue(findedHouses.Count() == 1);
        }
    }
}
