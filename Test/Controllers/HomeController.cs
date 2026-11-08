using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            // Try to load products from database using ADO.NET (Microsoft.Data.SqlClient).
            List<Product> items = new List<Product>();
            var connStr = _config.GetConnectionString("DefaultConnection");

            if (!string.IsNullOrWhiteSpace(connStr) && !connStr.Contains("PASSWORD_PLACEHOLDER"))
            {
                try
                {
                    using var conn = new SqlConnection(connStr);
                    conn.Open();
                    using var cmd = new SqlCommand("SELECT Id, Name, LastName, Price, HireDate FROM Product", conn);
                    using var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var idOrd = reader.GetOrdinal("Id");
                        var nameOrd = reader.GetOrdinal("Name");
                        var lastNameOrd = reader.GetOrdinal("LastName");
                        var priceOrd = reader.GetOrdinal("Price");
                        var hireDateOrd = reader.GetOrdinal("HireDate");

                        // Safely read Price which may be stored as different types (decimal, numeric, or string)
                        decimal priceValue = 0m;
                        if (!reader.IsDBNull(priceOrd))
                        {
                            var raw = reader.GetValue(priceOrd);
                            if (raw is decimal dec) priceValue = dec;
                            else if (raw is double d) priceValue = (decimal)d;
                            else if (raw is float f) priceValue = (decimal)f;
                            else
                            {
                                var s = Convert.ToString(raw);
                                if (!decimal.TryParse(s, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.InvariantCulture, out priceValue))
                                {
                                    decimal.TryParse(s, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out priceValue);
                                }
                            }
                        }

                        items.Add(new Product
                        {
                            Id = reader.IsDBNull(idOrd) ? 0 : reader.GetInt32(idOrd),
                            Name = reader.IsDBNull(nameOrd) ? null : reader.GetString(nameOrd),
                            LastName = reader.IsDBNull(lastNameOrd) ? null : reader.GetString(lastNameOrd),
                            Price = priceValue,
                            HireDate = reader.IsDBNull(hireDateOrd) ? (DateTime?)null : reader.GetDateTime(hireDateOrd)
                        });
                    }
                }
                catch(Exception ex)
                {
                    // If DB access fails, fall back to an in-memory sample list below.
                    items.Clear();
                }   
            }

            // If no items from DB, use an in-memory sample list
            //if (items.Count == 0)
            //{
            //    items = new List<Product>();
            //    for (int i = 1; i <= 23; i++)
            //    {
            //        items.Add(new Product { Id = i, Name = $"Product {i}", Price = Math.Round(10 + i * 1.5m, 2) });
            //    }
            //}

            var totalCount = items.Count;
            page = Math.Max(1, page);
            pageSize = Math.Max(1, pageSize);
            var totalPages = totalCount == 0 ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize);
            if (page > totalPages) page = totalPages;

            var pagedItems = items.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var model = new Test.Models.ProductListViewModel
            {
                Items = pagedItems,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalCount,
                TotalPages = totalPages
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
