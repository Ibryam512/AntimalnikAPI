using AntimalnikAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AntimalnikAPI.DAL.Repositories.Interfaces
{
    public interface IPostRepository
    {
        List<Post> GetPosts();
        List<Post> GetPosts(Expression<Func<Post, bool>> predicate);
        Post GetPost(string id);
        Post GetPost(Expression<Func<Post, bool>> predicate);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(Post post);
    }
}
