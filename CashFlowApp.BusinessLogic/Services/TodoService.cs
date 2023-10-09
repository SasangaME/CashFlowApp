namespace CashFlowApp.BusinessLogic.Services;

using System.Text.Json.Serialization;
using CashFlowApp.Models.DTOs;
using Newtonsoft.Json;

public interface ITodoService
{
    Task<TodoDto?> GetTodo(int id);
}

public class TodoService : ITodoService
{
    private readonly HttpClient _httpClient;

    public TodoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/todos/");
    }

    public async Task<TodoDto?> GetTodo(int id)
    {
        var httpResponse = await _httpClient.GetStringAsync(id.ToString());
        var todo = JsonConvert.DeserializeObject<TodoDto>(httpResponse);
        return todo;
    }
}