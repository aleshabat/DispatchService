using System.Linq;
using dispatchservice.api.Database;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using NUnit.Framework;

namespace dispatchservice.api.tests
{
    [TestFixture]
    public class DictionarysRpositoryTests: TestFixture
    {
        [Test]
        public void InsertUpdateDeleteStreetTest()
        {
            IStreetRepository rep = new StreetRepository { Connection = Connection };

            var street1 = new Street {Name = "Улица1", Type = "ул1"};
            rep.SaveOrUpdate(street1);
            var savedStreet = rep.All().FirstOrDefault();
            Assert.AreEqual("Улица1", savedStreet.Name);
            Assert.AreEqual("ул1", savedStreet.Type);

            savedStreet.Name = "Улица2";
            savedStreet.Type = "ул2";
            rep.SaveOrUpdate(savedStreet);
            savedStreet = rep.All().FirstOrDefault();
            Assert.AreEqual("Улица2", savedStreet.Name);
            Assert.AreEqual("ул2", savedStreet.Type);

            rep.Delete(savedStreet);
            Assert.IsTrue(!rep.All().Any());
        }

        [Test]
        public void GetListStreetTest()
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
                Estate = savedEstate,
                Deleted = true
            };
            repHouse.SaveOrUpdate(house1);

            var street = repStreet.GetList(estate1.Id).ToList();

            Assert.AreEqual("Улица1", street[0].Name);
        }

        [Test]
        public void InsertUpdateDeleteEstateTest()
        {
            var rep = new Repository<Estate> { Connection = Connection };

            var estate1 = new Estate { Name = "Район1" };
            rep.SaveOrUpdate(estate1);
            var savedEstate = rep.All().FirstOrDefault();
            Assert.AreEqual("Район1", savedEstate.Name);

            savedEstate.Name = "Район2";
            rep.SaveOrUpdate(savedEstate);
            savedEstate = rep.All().FirstOrDefault();
            Assert.AreEqual("Район2", savedEstate.Name);

            rep.Delete(savedEstate);
            Assert.IsTrue(!rep.All().Any());
        }

        [Test]
        public void InsertUpdateDeleteServiceTest()
        {
            var rep = new Repository<Service> { Connection = Connection };

            var service1 = new Service { Name = "Услуга1", Price = (decimal)15.2, Deleted = true};
            rep.SaveOrUpdate(service1);
            var savedService = rep.All().FirstOrDefault();
            Assert.AreEqual("Услуга1", savedService.Name);
            Assert.AreEqual((decimal)15.2, savedService.Price);
            Assert.AreEqual(true, savedService.Deleted);

            savedService.Name = "Услуга2";
            savedService.Price = (decimal) 10.5;
            savedService.Deleted = false;
            rep.SaveOrUpdate(savedService);
            savedService = rep.All().FirstOrDefault();
            Assert.AreEqual("Услуга2", savedService.Name);
            Assert.AreEqual((decimal)10.5, savedService.Price);
            Assert.AreEqual(false, savedService.Deleted);

            rep.Delete(savedService);
            Assert.IsTrue(!rep.All().Any());

        }

        [Test]
        public void InsertUpdateDeleteInjenerTest()
        {
            var rep = new Repository<Injener> { Connection = Connection };

            var injener1 = new Injener { Name = "Инженер1", Deleted = true };
            rep.SaveOrUpdate(injener1);
            var savedInjener = rep.All().FirstOrDefault();
            Assert.AreEqual("Инженер1", savedInjener.Name);
            Assert.AreEqual(true, savedInjener.Deleted);

            savedInjener.Name = "Инженер2";
            savedInjener.Deleted = false;
            rep.SaveOrUpdate(savedInjener);
            savedInjener = rep.All().FirstOrDefault();
            Assert.AreEqual("Инженер2", savedInjener.Name);
            Assert.AreEqual(false, savedInjener.Deleted);

            rep.Delete(savedInjener);
            Assert.IsTrue(!rep.All().Any());

        }

    }
}
