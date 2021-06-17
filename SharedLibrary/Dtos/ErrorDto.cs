using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Dtos
{
    public class ErrorDto
    {
        public List<string> Errors { get; private set; }
        public bool IsShow { get; private set; }

        private ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error, bool isShow)
        {
            Errors = new List<string>();

            this.Errors.Add(error);
            this.IsShow = isShow;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = new List<string>();

            this.Errors.AddRange(errors);
            this.IsShow = isShow;
        }
    }
}
