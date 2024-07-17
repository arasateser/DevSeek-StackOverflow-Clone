using Microsoft.AspNetCore.Mvc;
using DevSeek.Models;
using Microsoft.AspNetCore.Authorization;

namespace DevSeek.Controllers
{
    [Authorize]
    public class VotesController : Controller
    {
        // In-memory data storage
        private static List<Vote> _votes = new List<Vote>();
        private static List<User> _users = new List<User>(); // Assuming this is the same list used in UsersController
        private static List<Question> _questions = new List<Question>(); // Assuming this is the same list used in QuestionsController
        private static List<Comment> _comments = new List<Comment>(); // Assuming this is the same list used in CommentsController

        // POST: /Votes/UpvoteQuestion/{questionId}
        [HttpPost]
        public IActionResult UpvoteQuestion(int questionId)
        {
            // Find the current logged-in user
            var userName = HttpContext.Session.GetString("UserName");
            var user = _users.FirstOrDefault(u => u.UserName == userName);

            // Find the question to upvote
            var question = _questions.FirstOrDefault(q => q.Id == questionId);

            if (user != null && question != null)
            {
                // Create a new vote, set its properties, and add it to the user's, question's, and global lists
                var vote = new Vote { IsUpvote = true, User = user, Question = question };
                user.Votes.Add(vote);
                question.Votes.Add(vote);
                _votes.Add(vote);
            }

            // Redirect to the question details page
            return RedirectToAction("Details", "Questions", new { id = questionId });
        }

        // POST: /Votes/DownvoteQuestion/{questionId}
        [HttpPost]
        public IActionResult DownvoteQuestion(int questionId)
        {
            // Similar implementation to UpvoteQuestion but with IsUpvote set to false
            var userName = HttpContext.Session.GetString("UserName");
            var user = _users.FirstOrDefault(u => u.UserName == userName);
            var question = _questions.FirstOrDefault(q => q.Id == questionId);

            if (user != null && question != null)
            {
                var vote = new Vote { IsUpvote = false, User = user, Question = question };
                user.Votes.Add(vote);
                question.Votes.Add(vote);
                _votes.Add(vote);
            }

            return RedirectToAction("Details", "Questions", new { id = questionId });
        }

        // POST: /Votes/UpvoteComment/{commentId}
        [HttpPost]
        public IActionResult UpvoteComment(int commentId)
        {
            // Find the current logged-in user
            var userName = HttpContext.Session.GetString("UserName");
            var user = _users.FirstOrDefault(u => u.UserName == userName);

            // Find the comment to upvote
            var comment = _comments.FirstOrDefault(c => c.Id == commentId);

            if (user != null && comment != null)
            {
                // Create a new vote, set its properties, and add it to the user's, comment's, and global lists
                var vote = new Vote { IsUpvote = true, User = user, Comment = comment };
                user.Votes.Add(vote);
                comment.Votes.Add(vote);
                _votes.Add(vote);
            }

            // Redirect to the question details page
            return RedirectToAction("Details", "Questions", new { id = comment.Question.Id });
        }

        // POST: /Votes/DownvoteComment/{commentId}
        [HttpPost]
        public IActionResult DownvoteComment(int commentId)
        {
            // Similar implementation to UpvoteComment but with IsUpvote set to false
            var userName = HttpContext.Session.GetString("UserName");
            var user = _users.FirstOrDefault(u => u.UserName == userName);
            var comment = _comments.FirstOrDefault(c => c.Id == commentId);

            if (user != null && comment != null)
            {
                var vote = new Vote { IsUpvote = false, User = user, Comment = comment };
                user.Votes.Add(vote);
                comment.Votes.Add(vote);
                _votes.Add(vote);
            }

            return RedirectToAction("Details", "Questions", new { id = comment.Question.Id });
        }
    }
}
