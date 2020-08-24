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
    public class QuestionProfile : Profile
    {
		public QuestionProfile()
		{
			CreateMap<EQuestion, QuestionViewModel>()
				.ReverseMap();
		}
    }
}
