using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextGen.Model.ViewModel
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public IList<string> Messages { get; private set; }

        public ErrorViewModel() : this(new List<string>())
        {
        }

        public ErrorViewModel(IList<string> messages)
        {
            Messages = messages;
        }
    }
}
