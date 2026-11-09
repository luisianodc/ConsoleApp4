using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    /// <summary>
    /// Represents a rocket stored in the application's database.
    /// </summary>
    public class Rocket
    {
        /// <summary>
        /// Gets or sets the database-generated rocket identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the rocket name.
        /// </summary>
        public string RocketName { get; set; }

        /// <summary>
        /// Gets or sets the rocket manufacturer.
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Gets or sets the crew members assigned to this rocket.
        /// </summary>
        public ICollection<RocketCrew> RocketCrews { get; set; }

    }
}
