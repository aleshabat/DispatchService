using System.Collections.Generic;

namespace dispatchservice.web.Models.ReportParams
{
    public class ReportParamsViewModel
    {
        public ReportParamsViewModel(){}

        public string ReportCaption { get; set; }

        private List<ParamDetail> _params;
        public List<ParamDetail> Params
        {
            get
            {
                for (int i = 0; _params != null && i < _params.Count; i++)
                {
                    if (_params[i].Model!= null)
                        _params[i].Model.Name = string.Format("Params[{0}].ReturnValue", i);
                }
                return _params;
            }
            set { _params = value; }
        }
        public string RenderReportActionName { get; set; }

    }

    public class ParamDetail
    {
        public ParamDetail() { }

        public HtmlElement Model { get; set; }
        public string PartialViewName { get; set; }
        public string ReturnValue { get; set; }

    }
}