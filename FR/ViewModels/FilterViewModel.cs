using System;
using System.ComponentModel.DataAnnotations;

namespace FR.Api.ViewModels
{
    public class FilterViewModel
    {
        private DateTime? _start = null;
        private DateTime? _end = null;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public DateTime Start {
            get
            {
                return _start ?? DateTime.MinValue;
            }
            set
            {
                _start = value;
            }
        }
        
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public DateTime End {
            get
            {
                return _end ?? DateTime.MaxValue;
            }
            set
            {
                _end = value;
            }
        }
        public string Group { get; set; }
        public string Team { get; set; }
    }
}