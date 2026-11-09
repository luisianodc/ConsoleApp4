
namespace ConsoleApp4
{
    /// <summary>
    /// Represents a crew member assigned to a rocket.
    /// </summary>
    public class RocketCrew
    {
        /// <summary>
        /// Gets or sets the crew member's database identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the crew member's name.
        /// </summary>
        public string CrewName { get; set; }

        /// <summary>
        /// Gets or sets the optional identifier of the assigned rocket.
        /// </summary>
        public int? RocketId { get; set; }

        /// <summary>
        /// Gets or sets the rocket associated with this crew member.
        /// </summary>
        public Rocket Rocket { get; set; }
    }
}
