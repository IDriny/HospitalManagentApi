namespace HospitalManagentApi.Core.Domain
{
    public class Clinic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<ClinicDoctor> Doctors { get; set; }

    }
}
