﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperLib.IService;
using DapperLib.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperDemo.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperController : ControllerBase
    {
        private readonly DapperClient _oracleClient;
        private readonly DapperClient _mssqlClient;
        public DapperController(IDapperFactory dapperFactory)
        {
            _oracleClient = dapperFactory.CreateClient("OracleDB");
            _mssqlClient = dapperFactory.CreateClient("MSSqlDB");
        }
        [HttpGet("GetAllUser")]
        public List<dynamic> GetAllUser()
        {
            dynamic list = _mssqlClient.Query<dynamic>(@"select * from usertest");
            return list;
        }
        [HttpGet("GetAllPerson")]
        public List<dynamic> GetAllPerson()
        {
            dynamic list = _mssqlClient.Query<dynamic>(@"select * from Person");
            return list;
        }
        [HttpPost("GetByID")]
        public dynamic GetByID(int id)
        {
            dynamic item = _mssqlClient.Query<dynamic>("select * from Person where ID=@id", new { id = id });
            return item;
        }
        [HttpPost("GetByDiscrimitor")]
        public List<dynamic> GetByDiscrimitor(string discrimitor)
        {
            dynamic list = _mssqlClient.Query<dynamic>("select * from Person where Discriminator=@discriminator", new { discriminator = discrimitor });
            return list;
        }
    }
}
