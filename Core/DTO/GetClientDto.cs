using Core.Models;

namespace Core.DTO
{
    public class GetClientDto
    {
        public GetClientDto(Client client)
        {
            id = client.id;
            FirstName = client.FirstName;
            SecondName = client.SecondName;
            BirthDate = client.BirthDate;
        }

        public GetClientDto(int id, string FirstName, string SecondName, DateTime BirthDate)
        {
            this.id = id;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.BirthDate = BirthDate;
        }

        public int id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public DateTime BirthDate { get; set; }

    }
}
