using System;
using System.Linq;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using NUnit.Framework;

namespace dispatchservice.api.tests
{
    [TestFixture]
    public class InjenerHouseRpositoryTests: TestFixture
    {
        [Test]
        public void InsertUpdateDeleteInjenerHouseTest()
        {
            InjenerHouseRepository repInjenerHouse = new InjenerHouseRepository { Connection = Connection };
            IHouseRepository repHouse = new HouseRpository { Connection = Connection };
            var repInjener = new Repository<Injener> { Connection = Connection };
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
                Estate = savedEstate,
                Deleted = true
            };
            repHouse.SaveOrUpdate(house1);

            var house2 = new House
            {
                Number = "дом2",
                Street = savedstreet,
                Estate = savedEstate,
                Deleted = false
            };
            repHouse.SaveOrUpdate(house2);

            var injener1 = new Injener
            {
                Name = "Инженер1",
                Deleted = true
            };
            repInjener.SaveOrUpdate(injener1);

            var injener2 = new Injener
            {
                Name = "Инженер2",
                Deleted = true
            };
            repInjener.SaveOrUpdate(injener2);
            //-------------------------------------------------

            var injenerHouse = new InjenerHouse
            {
                Injener = injener1,
                House = house1
            };

            repInjenerHouse.SaveOrUpdate(injenerHouse);
            var savedInjenerHouse = repInjenerHouse.All().FirstOrDefault();

            Assert.AreEqual("дом1", savedInjenerHouse.House.Number);
            Assert.AreEqual("Инженер1", savedInjenerHouse.Injener.Name);

            savedInjenerHouse.House = house2;
            savedInjenerHouse.Injener = injener2;
            repInjenerHouse.SaveOrUpdate(savedInjenerHouse);
            savedInjenerHouse = repInjenerHouse.All().FirstOrDefault();

            Assert.AreEqual("дом2", savedInjenerHouse.House.Number);
            Assert.AreEqual("Инженер2", savedInjenerHouse.Injener.Name);

            repInjenerHouse.Delete(savedInjenerHouse);
        }

        [Test]
        public void GetInjenerHouseTest()
        {
            InjenerHouseRepository repInjenerHouse = new InjenerHouseRepository { Connection = Connection };
            IHouseRepository repHouse = new HouseRpository { Connection = Connection };
            var repInjener = new Repository<Injener> { Connection = Connection };
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
                Estate = savedEstate,
                Deleted = true
            };
            repHouse.SaveOrUpdate(house1);

            var injener1 = new Injener
            {
                Name = "Инженер1",
                Deleted = true
            };
            repInjener.SaveOrUpdate(injener1);

            //-------------------------------------------------

            var injenerHouse = new InjenerHouse
            {
                Injener = injener1,
                House = house1
            };

            repInjenerHouse.SaveOrUpdate(injenerHouse);
            var savedInjenerHouse = repInjenerHouse.Get(injenerHouse.Id);

            Assert.AreEqual(injenerHouse.Id, savedInjenerHouse.Id);

            Assert.AreEqual(injenerHouse.Injener.Id, injener1.Id);
            Assert.AreEqual(injenerHouse.Injener.Name, injener1.Name);
            Assert.AreEqual(injenerHouse.Injener.Deleted, injener1.Deleted);

            Assert.AreEqual(injenerHouse.House.Id, house1.Id);
            Assert.AreEqual(injenerHouse.House.Number, house1.Number);
            Assert.AreEqual(injenerHouse.House.Deleted, house1.Deleted);

            Assert.AreEqual(injenerHouse.House.Street.Id, house1.Street.Id);
            Assert.AreEqual(injenerHouse.House.Street.Name, house1.Street.Name);
            Assert.AreEqual(injenerHouse.House.Estate.Id, house1.Estate.Id);
            Assert.AreEqual(injenerHouse.House.Estate.Name, house1.Estate.Name);
        }
   
    }
}
