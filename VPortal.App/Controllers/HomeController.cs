﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VPortal.Core.Data.Crud.Attributes;
using VPortal.Core.Data.Crud;
using VPortal.App.Services;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using VPortal.Core.Log;

namespace VPortal.App.Controllers
{

    [Table("TestTable")]
    public class TestTable 
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        [IgnoreAll]
        public int RowTotal {get; set;}
    }


     [Table("NewTable")]
    public class NewTable 
    {
        [Key]
        public int Id { get; set; }
        public string user_name { get; set; }
        public string user_address { get; set; }

        [IgnoreAll]
        public int RowTotal {get; set;}
    }

    public class HomeController : Controller
    {
      //  private ITestTableService _testService;
        //private INewTableService _newService;
    //    private ILogger _logger;

        // public HomeController(ITestTableService testService, ILogger logger,INewTableService newService)
        // {
        //     _testService = testService;
        //     _logger = logger;
        //     _newService = newService;
        // }
        
        public IActionResult Index()
        {

            try
            {
                int a = 5;
                int div = a / 0;

             //   _logger.Log(LogType.Info, () => "The division of {a} by 0 is :" + div.ToString());
            }
            catch (Exception)
            {
                // Logger takes logtype, log status as a function and the exception message
            //    _logger.Log(LogType.Error, () => ex.ToString(),ex.Message);
            }

            // get record by id
         //   var d = _testService.CrudService.Get("where name = @name",new { @name = "manoj" });

        //    _logger.Log(LogType.Info, () => "Data Got successfully");
//
            // new model to insert into database
            var model = new TestTable()
            {
                UserName = "manoj",
                Email = "manoj@gmail.com"
            };

            //var data = _testService.CrudService.Insert(model);

            // model to update
            var modelUpdate = new TestTable()
            {
                Id = 102,
                UserName = "rakesh",
                Email = "rakesh@gmail.com"
            };

            //_testService.CrudService.Update(modelUpdate);



            // remove the data from db
            //_testService.CrudService.Delete(104);

           
            return View();
        }

        public IActionResult About()
        {

            // get list of data using async
           // var data = await _testService.CrudService.GetListAsync();

            // get data by pagination (pageNum,rowsPerpage,pageSize,conditions,orderby,parameters)
           // var pagdata1 = await _testService.CrudService.GetListPagedAsync(1, 10, 1, "", "");
          //  var pagdata2 = await _testService.CrudService.GetListPagedAsync(10, 10, 1, "", "");

            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {

            // new model to insert into database
            // var model = new NewTable()
            // {
            //     user_name = "manoj",
            //     user_address = "lokanthai"
            // };

            // var data = _newService.NewCrudService.Insert(model);

            // ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
