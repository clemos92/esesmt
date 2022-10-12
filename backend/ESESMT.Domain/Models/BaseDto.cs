using System;
using System.Collections.Generic;
using System.Text;

namespace ESESMT.Domain.Models
{
    public class BaseDto<TEntityKey>
    {
        public TEntityKey Id { get; set; }
    }
}
