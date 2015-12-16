﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using PartsUnlimited.Models;
using System.Data.Entity;

namespace PartsUnlimited.WebJobs.UpdateProductInventory
{
    public class Functions
    {
        public async static Task UpdateProductProcessTaskAsync([QueueTrigger("product")] ProductMessage message)
        {
            using (var context = new PartsUnlimitedContext())
            {
                var dbProductList = await context.Products.ToListAsync();
                foreach (var queueProduct in message.ProductList)
                {
                    var dbProduct = dbProductList.SingleOrDefault(x => x.SkuNumber == queueProduct.SkuNumber);

                    if (dbProduct != null)
                    {
                        dbProduct.Inventory = queueProduct.Inventory;
                        dbProduct.LeadTime = queueProduct.LeadTime;
                    }
                }
                await context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}