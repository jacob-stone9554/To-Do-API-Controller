namespace ToDoAPI_Controller.Models
{
    /*
     * Item represents a record that will hold information about a given taks.
     * Information includes Id, Name, Description, Type (like category) and isCompleted (true/false)
     */
    public record Item
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public string ? Description { get; set; }
        public string ? Type { get; set; }
        public bool isCompleted { get; set; }
    }
}
