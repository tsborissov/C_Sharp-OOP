using InversionOfControlContainer.Contracts;
using InversionOfControlContainer.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InversionOfControlContainer
{
    class StartUp
    {
        public static void Main(string[] args)
        {
            IServiceProvider service = new ServiceCollection()
                .AddScoped<IReader, Reader>()
                .AddScoped<IWriter, Writer>()
                .AddScoped<Copy, Copy>()
                .BuildServiceProvider();

            Copy cp = service.GetService<Copy>();

            cp.CopyAllCharacters();
        }
    }
}
