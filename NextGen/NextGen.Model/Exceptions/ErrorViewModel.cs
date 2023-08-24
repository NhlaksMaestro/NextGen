using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Model.Exceptions
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);

        public IList<string> Messages { get; private set; }

        public ErrorViewModel()
          : this((IList<string>)new List<string>())
        {
        }

        public ErrorViewModel(IList<string> messages) => this.Messages = messages;
    }
}
