// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Azure.WebJobs;

namespace PartsUnlimited.WebJobs.UpdateProductInventory
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            var jobHostConfig = new JobHostConfiguration();
            var host = new JobHost(jobHostConfig);

            host.RunAndBlock();
            return 0;
        }
    }
}
