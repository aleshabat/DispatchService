using System;
using System.Linq;
using dispatchservice.api.Domain;
using dispatchservice.api.Repositories;
using NUnit.Framework;

namespace dispatchservice.api.tests
{
    [TestFixture]
    public class TicketRpositoryTests: TestFixture
    {
        [Test]
        public void Test()
        {
            ITicketRepository repTicket = new TicketRepository { Connection = Connection };
            var tikets = repTicket.GetList(new Guid("cdf39457-6040-440f-bf58-a44e011b1444"), new Guid("cdf39457-6040-440f-bf58-a44e011b1442"), null, null);
        }

        [Test]
        public void InsertUpdateDeleteTicketTest()
        {
            ITicketRepository repTicket = new TicketRepository { Connection = Connection };
            var repService = new Repository<Service> { Connection = Connection };

            var injenerHouse1 = CreateInjenerHouse("Инженер1", "Улица1", "ул", "Район1", "мкрн", "дом1");
            var injenerHouse2 = CreateInjenerHouse("Инженер2", "Улица2", "ул", "Район2", "мкрн", "дом2");

            //-------------------------------------------------
            var service1 = new Service
            {
                Name = "Услуга1",
                Deleted = true,
                Price = (decimal)123.65
            };
            var service2 = new Service
            {
                Name = "Услуга2",
                Deleted = false,
                Price = (decimal)4765.23
            };
            repService.SaveOrUpdate(service1);
            repService.SaveOrUpdate(service2);

            //-------------------------------------------------
            var ticket1 = new Ticket
            {
                Injener = injenerHouse1.Injener,
                House = injenerHouse1.House,
                Service = service1,
                Flat = "123",
                Price = (decimal)145.25,
                DateTime = DateTime.Now,
                DateCancel = DateTime.Now.Date,
                DateExecute = DateTime.Now.Date,
                Phone = "3546547",
                MobilePhone = "89254545454"
            };
            repTicket.SaveOrUpdate(ticket1);
            var savedTicket = repTicket.All().FirstOrDefault();
            VerificationTicket(ticket1, savedTicket);

            //-------------------------------------------------
            savedTicket.Injener = injenerHouse2.Injener;
            savedTicket.House = injenerHouse2.House;
            savedTicket.Service = service2;
            savedTicket.Flat = "456";
            savedTicket.Price = (decimal) 5454.23;
            savedTicket.DateTime = DateTime.Now.AddDays(2);
            savedTicket.DateCancel = null;
            savedTicket.DateExecute = DateTime.Now.AddDays(2);
            savedTicket.Phone = "799456";
            savedTicket.MobilePhone = "8927155418";

            repTicket.SaveOrUpdate(savedTicket);
            var updatedTicket = repTicket.All().FirstOrDefault();

            VerificationTicket(savedTicket, updatedTicket);
            //-------------------------------------------------

            savedTicket.Injener = null;

            repTicket.SaveOrUpdate(savedTicket);
            updatedTicket = repTicket.All().FirstOrDefault();

            VerificationTicket(savedTicket, updatedTicket);
            //-------------------------------------------------

            //repTicket.Delete(updatedTicket);
            Assert.IsTrue(!repTicket.All().Any());
        }

        [Test]
        public void GetTicketTest()
        {
            ITicketRepository repTicket = new TicketRepository { Connection = Connection };
            var repService= new Repository<Service> { Connection = Connection };

            var injenerHouse = CreateInjenerHouse("Инженер1", "Улица1","ул", "Район1", "мкрн", "дом1");

            //-------------------------------------------------
            var service = new Service
            {
                Name = "Услуга1",
                Deleted = true,
                Price = (decimal)123.65
            };
            repService.SaveOrUpdate(service);

            //-------------------------------------------------
            var ticket = new Ticket
            {
                Injener = injenerHouse.Injener,
                House = injenerHouse.House,
                Service = service,
                Flat = "123",
                Price = (decimal)145.25,
                DateTime = DateTime.Now,
                DateCancel = DateTime.Now.Date,
                DateExecute = DateTime.Now.Date,
                Phone = "3546547",
                MobilePhone = "89254545454"
            };
            repTicket.SaveOrUpdate(ticket);
            var savedTicket = repTicket.Get(ticket.Id);

            VerificationTicket(ticket, savedTicket);
        }

        [Test]
        public void GetListTicketTest()
        {
            ITicketRepository repTicket = new TicketRepository { Connection = Connection };
            var repService = new Repository<Service> { Connection = Connection };

            var injenerHouse = CreateInjenerHouse("Инженер1", "Улица1", "ул", "Район1","мкрн", "дом1");

            //-------------------------------------------------
            var service = new Service
            {
                Name = "Услуга1",
                Deleted = true,
                Price = (decimal)123.65
            };
            repService.SaveOrUpdate(service);

            //-------------------------------------------------
            var ticket = new Ticket
            {
                Injener = injenerHouse.Injener,
                House = injenerHouse.House,
                Service = service,
                Flat = "123",
                Price = (decimal)145.25,
                DateTime = DateTime.Now,
                DateCancel = DateTime.Now.Date,
                DateExecute = DateTime.Now.Date,
                Phone = "3546547",
                MobilePhone = "89254545454"
            };
            repTicket.SaveOrUpdate(ticket);

            var tickets = repTicket.GetList(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), null).ToList();
            Assert.IsTrue(tickets.Any());

            tickets = repTicket.GetList(null, DateTime.Now.AddDays(1), null).ToList();
            Assert.IsTrue(tickets.Any());

            tickets = repTicket.GetList(null, null, null).ToList();
            Assert.IsTrue(tickets.Any());

            tickets = repTicket.GetList(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(-1), null).ToList();
            Assert.IsTrue(!tickets.Any());
        }
        private void VerificationTicket(Ticket expected, Ticket actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Flat, actual.Flat);
            Assert.AreEqual(expected.Price, actual.Price);
            Assert.AreEqual(expected.DateTime, actual.DateTime);
            Assert.AreEqual(expected.DateCancel, actual.DateCancel);
            Assert.AreEqual(expected.DateExecute, actual.DateExecute);
            Assert.AreEqual(expected.Phone, actual.Phone);
            Assert.AreEqual(expected.MobilePhone, actual.MobilePhone);

            // Service - проверка
            Assert.AreEqual(expected.Service.Id, actual.Service.Id);
            Assert.AreEqual(expected.Service.Name, actual.Service.Name);
            Assert.AreEqual(expected.Service.Price, actual.Service.Price);
            Assert.AreEqual(expected.Service.Deleted, actual.Service.Deleted);

            // Injener- проверка
            if (actual.Injener != null && expected.Injener != null)
            {
                Assert.AreEqual(expected.Injener.Id, actual.Injener.Id);
                Assert.AreEqual(expected.Injener.Name, actual.Injener.Name);
                Assert.AreEqual(expected.Injener.Deleted, actual.Injener.Deleted);
            }
            else
            {
                Assert.IsNull(expected.Injener);
                Assert.IsNull(actual.Injener);
            }

            // House - проверка
            Assert.AreEqual(expected.House.Id, actual.House.Id);
            Assert.AreEqual(expected.House.Number, actual.House.Number);
            Assert.AreEqual(expected.House.Deleted, actual.House.Deleted);

            Assert.AreEqual(expected.House.Street.Id, actual.House.Street.Id);
            Assert.AreEqual(expected.House.Street.Name, actual.House.Street.Name);
            Assert.AreEqual(expected.House.Street.Type, actual.House.Street.Type);
            Assert.AreEqual(expected.House.Estate.Id, actual.House.Estate.Id);
            Assert.AreEqual(expected.House.Estate.Name, actual.House.Estate.Name);
            Assert.AreEqual(expected.House.Estate.Type, actual.House.Estate.Type);

        }

        private InjenerHouse CreateInjenerHouse(string injenerName, string streetName, string streetType, string estateName, string estateType, string houseNumber)
        {
            InjenerHouseRepository repInjenerHouse = new InjenerHouseRepository { Connection = Connection };
            IHouseRepository repHouse = new HouseRpository { Connection = Connection };
            var repInjener = new Repository<Injener> { Connection = Connection };
            IStreetRepository repStreet = new StreetRepository { Connection = Connection };
            var repEstate = new Repository<Estate> { Connection = Connection };

            var street = new Street { Name = streetName, Type = streetType};
            repStreet.SaveOrUpdate(street);
            var savedstreet = repStreet.All().FirstOrDefault();

            var estate = new Estate { Name = estateName, Type = estateType };
            repEstate.SaveOrUpdate(estate);
            var savedEstate = repEstate.All().FirstOrDefault();

            var house = new House
            {
                Number = houseNumber,
                Street = savedstreet,
                Estate = savedEstate,
                Deleted = true
            };
            repHouse.SaveOrUpdate(house);

            //-------------------------------------------------
            var injener = new Injener
            {
                Name = injenerName,
                Deleted = true
            };
            repInjener.SaveOrUpdate(injener);

            //-------------------------------------------------
            var injenerHouse = new InjenerHouse
            {
                Injener = injener,
                House = house
            };
            repInjenerHouse.SaveOrUpdate(injenerHouse);

            return injenerHouse;
        }
   
    }
}
