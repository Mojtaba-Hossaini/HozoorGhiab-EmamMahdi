﻿using HozoorGhiabEmamMahdi.Models;
using HozoorGhiabEmamMahdi.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HozoorGhiabEmamMahdi.Controllers
{
    public class HozoorController : Controller
    {
        private readonly HozoorContext context;

        public HozoorController(HozoorContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult>NewHozoor(int darsId, string dateTime)
        {
            DateTime tarikh = Convert.ToDateTime(dateTime);
            
            var oldHozoor = await context.Hozoors.Where(c => c.Tarikh == tarikh).FirstOrDefaultAsync();
            if (oldHozoor != null)
                return NotFound();
            var users = context.Doroos_Users.Where(c => c.DoroosId == darsId).ToList();
            var dars = await context.Dorooses.FindAsync(darsId);
            List<User> UsersFind = new List<User>();
            foreach (var item in users)
            {
                var userTofind = context.Users.Find(item.UserId);
                UsersFind.Add(userTofind);
            }

            HozoorViewModel hozoorSent = new HozoorViewModel
            {
                Dars = dars,
                Users = UsersFind

            };


            return View(hozoorSent);
        }
    }
}