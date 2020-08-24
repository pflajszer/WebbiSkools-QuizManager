using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.BRL.ViewModels.Quiz;
using WebbiSkools.QuizManager.DAL.Entities.QuizManager;

namespace WebbiSkools.QuizManager.BRL.AutomapperProfiles
{
    public class AnswerProfile : Profile
    {
		public AnswerProfile()
		{
			CreateMap<EAnswer, AnswerViewModel>()
				//.ConstructUsing(x => new AnswerViewModel(x.AnswerText, x.IsCorrect))
				//.ForMember(x => x.QuestionId, x => x.MapFrom(x => x.Question.Id))
				//.ForMember(x => x.QuestionId, x => x.Ignore())
				.ReverseMap();
		} 
    }
}
