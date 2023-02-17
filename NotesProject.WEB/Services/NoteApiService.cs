using NotesProject.Core.DTOs;
using NotesProject.Shared.Dtos;

namespace NotesProject.WEB.Services
{
    public class NoteApiService
    {
        private readonly HttpClient _httpClient;

        public NoteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<NoteDto>> GetByIdNotesAsyn(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseDto<IEnumerable<NoteDto>>>($"notes/{id}");
            if (response == null) return null;
            if (response.Error!= null)
            {
                return null;
            }
            return response.Data;
        }
        public async Task<NoteDto> AddAsync(CreateNoteDto dto)
        {
            var response = await _httpClient.PostAsJsonAsync("notes", dto);
            

            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<ResponseDto<NoteDto>>();

            return responseBody.Data;

        }
    }
}
