using InversionOfControlContainer.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace InversionOfControlContainer.Models
{
    public class Copy
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Copy(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void CopyAllCharacters()
        {
            string text = reader.Read();
            writer.Write(text);
        }

    }
}
