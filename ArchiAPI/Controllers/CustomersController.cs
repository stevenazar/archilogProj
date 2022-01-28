﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArchiAPI.Data;
using ArchiAPI.Models;
using ArchiLibrary.Controllers;

namespace ArchiAPI.Controllers 
{
    public class CustomersController : BaseController<ArchiDbContext, Customer>
    {
        
        public CustomersController(ArchiDbContext context): base(context)
        {

        }
        

    }
    
    
}
