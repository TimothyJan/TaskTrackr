namespace TaskTrackr.Server.DTOs
{
    public class ProjectTaskDTO
    {
        public int ProjectTaskId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AssignedUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int Progress { get; set; }
    }
}
