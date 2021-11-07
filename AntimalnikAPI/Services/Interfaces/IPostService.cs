using AntimalnikAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AntimalnikAPI.Services.Interfaces
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts();
        Task<Post> GetPost(string id);
        Task AddPost(Post post);
        Task EditPost(Post post);
        Task DeletePost(string id);
    }
}
