namespace JobPortal.WebModels.JobWebModels
{
    public class ListJobResponseWebModel
    {
        public int Total { get; set; }
        public IEnumerable<GetJobResponseWebModelMinimal>? Data { get; set; }
    }
}
