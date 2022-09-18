using AutoMapper;
using BlogApi.Data.Entities;
using BlogApi.Data.Repository;
using BlogApi.Dtos;
using FluentValidation;

namespace BlogApi.Endpoints
{
    public static class BlogPostEndpoints
    {
        public static WebApplication MapBlogPostEndpoints(this WebApplication app)
        {
            app.MapGet("/api/posts", async (IBlogPostRepository postRepository, IMapper mapper) =>
            {
                var posts = await postRepository.GetBlogPosts();
                var postsDto = mapper.Map<List<ReadPostDto>>(posts);
                return Results.Ok(postsDto);
            }).RequireAuthorization();

            app.MapGet("/api/posts/{id}", async (IBlogPostRepository postRepository, IMapper mapper, int id) =>
            {
                var post = await postRepository.GetBlogPostById(id);

                if (post is null)
                {
                    return Results.NotFound();
                }

                var readPostDto = mapper.Map<ReadPostDto>(post);
                return Results.Ok(readPostDto);
            }).RequireAuthorization();

            app.MapPost("/api/posts", async (IBlogPostRepository postRepository, IMapper mapper, IValidator<CreatePostDto> validator, CreatePostDto createPostDto) =>
            {
                var validationResult = validator.Validate(createPostDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new { errors = errors });
                }

                var post = mapper.Map<BlogPost>(createPostDto);
                post.CreatedAt = DateTime.Now;
                await postRepository.CreatePost(post);

                var createdPost = mapper.Map<ReadPostDto>(post);
                return Results.Created($"/api/posts/{createdPost.Id}", createdPost);
            }).RequireAuthorization();

            app.MapPut("/api/posts", async (IBlogPostRepository postRepository, IValidator<UpdatePostDto> validator, UpdatePostDto updatePostDto) =>
            {
                var validationResult = validator.Validate(updatePostDto);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(x => x.ErrorMessage);
                    return Results.BadRequest(new { errors = errors });
                }

                var post = await postRepository.GetBlogPostById(updatePostDto.Id);

                if (post is null)
                {
                    return Results.NotFound();
                }

                var updatedPost = updatePostDto.UpdateBlogPost(post);
                await postRepository.UpdateBlogPost(updatedPost);
                return Results.Ok();
            }).RequireAuthorization();

            app.MapDelete("/api/posts/{id}", async (IBlogPostRepository postRepository, int id) =>
            {
                var post = await postRepository.GetBlogPostById(id);

                if (post is null)
                {
                    return Results.NotFound();
                }

                await postRepository.DeleteBlogPost(post);
                return Results.Ok();
            }).RequireAuthorization();

            return app;
        }
    }
}
