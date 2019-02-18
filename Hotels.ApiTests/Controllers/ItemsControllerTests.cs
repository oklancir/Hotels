using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Hotels.ApiTests.Controllers
{
    [TestClass]
    public class ItemsControllerTests
    {
        [TestMethod]
        public async Task GetItemsTest_WhenCalled_ReturnsItemList()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/items");
            IEnumerable<ItemDto> items = null;

            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<IEnumerable<ItemDto>>();
            }

            Assert.IsInstanceOfType(items, typeof(IEnumerable<ItemDto>), "Items list request failed.");
        }

        [TestMethod]
        public async Task GetItem_WhenIdIsValid_ReturnsItemDto()
        {
            var id = 20;
            var client = GetHttpClient();
            ItemDto itemDto = null;

            var response = await client.GetAsync("api/items/" + id);
            if (response.IsSuccessStatusCode)
            {
                itemDto = await response.Content.ReadAsAsync<ItemDto>();
            }

            Assert.IsNotNull(itemDto, "Request failed.");
            Assert.AreEqual(itemDto.GetType(), typeof(ItemDto), "ItemDto not returned.");
        }
        [TestMethod]
        public async Task GetItem_WhenIdNotValid_ReturnsItemDto()
        {
            var id = -1;
            var client = GetHttpClient();
            ItemDto itemDto = null;

            var response = await client.GetAsync("api/items/" + id);
            if (response.IsSuccessStatusCode)
            {
                itemDto = await response.Content.ReadAsAsync<ItemDto>();
            }

            Assert.IsNull(itemDto, "Failed request.");
        }

        [TestMethod]
        public async Task CreateItem_WhenItemDtoIsValid_ReturnsItemDto()
        {
            var client = GetHttpClient();
            ItemDto itemDto = null;
            var itemToCreate = MockItem();
            var response = await client.PostAsJsonAsync("api/items", Mapper.Map<Item, ItemDto>(itemToCreate));

            if (response.IsSuccessStatusCode)
            {
                itemDto = await response.Content.ReadAsAsync<ItemDto>();
            }

            Assert.IsNotNull(itemDto, "Item is null");
            Assert.IsInstanceOfType(itemDto, typeof(ItemDto), "Item not added successfully");
        }

        [TestMethod]
        public async Task CreateItem_WhenItemDtoIsNull_ReturnsNullGuestObject()
        {
            var client = GetHttpClient();
            ItemDto itemDto = null;
            var response = await client.PostAsJsonAsync("api/items", Mapper.Map<Item, ItemDto>(null));

            if (response.IsSuccessStatusCode)
            {
                itemDto = await response.Content.ReadAsAsync<ItemDto>();
            }

            Assert.IsNull(itemDto, "Trying to post an item that is not null.");
        }

        [TestMethod]
        public async Task EditItem_WithVaildItemId_ReturnsUpdatedItemObject()
        {
            var client = GetHttpClient();
            ItemDto itemDto = null;
            var itemToUpdate = MockItemToDb();
            itemToUpdate.Quantity = 10;
            var response = await client.PutAsJsonAsync("api/items/" + itemToUpdate.Id, Mapper.Map<Item, ItemDto>(itemToUpdate));

            if (response.IsSuccessStatusCode)
            {
                itemDto = await response.Content.ReadAsAsync<ItemDto>();
            }

            Assert.IsNotNull(itemDto);
            Assert.AreEqual(10, itemDto.Quantity, "Item quantity not updated succesfully");
            Assert.IsInstanceOfType(itemDto, typeof(ItemDto), "Return ItemDto object failed.");
        }

        [TestMethod]
        public async Task DeleteItem_WhenCalledWithValidId_ReturnsDeletedObject()
        {
            var client = GetHttpClient();
            ItemDto itemDto = null;
            var itemToDelete = GetItemToDelete().GetAwaiter().GetResult();
            var response = await client.DeleteAsync($"api/items/{itemToDelete.Id}");

            if (response.IsSuccessStatusCode)
            {
                itemDto = await response.Content.ReadAsAsync<ItemDto>();
            }

            Assert.IsNotNull(itemDto);
            Assert.AreEqual(itemToDelete.Id, itemDto.Id, "Item Id is not valid");
            Assert.IsInstanceOfType(itemDto, typeof(ItemDto), "Item object not deleted successfully");
        }



        private HttpClient GetHttpClient()
        {
            var client = new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseAddress"]) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        private Item MockItem()
        {
            return new Item
            {
                Id = 8,
                ServiceProductId = 1,
                Quantity = 1,
                InvoiceId = 25
            };
        }

        private Item MockItemToDb()
        {
            var Context = new HotelsContext();

            var testItem = new Item
            {
                Id = 8,
                ServiceProductId = 1,
                Quantity = 1,
                InvoiceId = 25
            };

            if (Context.Items.Find(testItem.Id) == null)
            {
                Context.Items.Add(testItem);
                Context.SaveChanges();
            }

            return testItem;
        }

        private async Task<ItemDto> GetItemToDelete()
        {
            var client = GetHttpClient();
            var itemToCreate = MockItem();
            var response = await client.PostAsJsonAsync("api/items", Mapper.Map<Item, ItemDto>(itemToCreate));

            if (!response.IsSuccessStatusCode)
                return null;

            var itemDto = await response.Content.ReadAsAsync<ItemDto>();
            return itemDto;
        }
    }
}
