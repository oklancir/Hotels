﻿using AutoMapper;
using Hotels.Dtos;
using Hotels.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Hotels.Controllers.Api
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class ItemsController : ApiController
    {
        private readonly IHotelsContext Context;
        private readonly Logger Logger = LogManager.GetLogger("logfile");

        public ItemsController()
        {
            Context = new HotelsContext();
        }

        public ItemsController(IHotelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            return Context.Items.ToList().Select(Mapper.Map<Item, ItemDto>);
        }

        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            var item = Context.Items.SingleOrDefault(i => i.Id == id);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return Ok(Mapper.Map<Item, ItemDto>(item));
        }

        [HttpPost]
        public IHttpActionResult CreateItem(ItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var invoices = Context.Invoices.Select(i => i.Id).ToList();

            if (!invoices.Contains(itemDto.InvoiceId))
                return BadRequest("You need to add an item to an existing invoice.");

            var item = Mapper.Map<ItemDto, Item>(itemDto);
            Context.Items.Add(item);

            try
            {
                Context.SaveChanges();
                itemDto.Id = item.Id;
                return Created(new Uri(Request.RequestUri + "/" + item.Id), itemDto);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpPut]
        public IHttpActionResult EditItem(int id, ItemDto itemDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var itemInDb = Context.Items.SingleOrDefault(i => i.Id == id);

            if (itemInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(itemDto, itemInDb);

            try
            {
                Context.SaveChanges();
                return Ok(itemInDb);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }

        [HttpDelete]
        public IHttpActionResult DeleteItem(int id)
        {
            var itemInDb = Context.Items.SingleOrDefault(i => i.Id == id);

            if (itemInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Context.Items.Remove(itemInDb);

            try
            {
                Context.SaveChanges();
                return Ok(itemInDb);
            }
            catch (Exception e)
            {
                Logger.Error(e, e.Message);
                return BadRequest($"Error{e.Message}");
            }
        }
    }
}
