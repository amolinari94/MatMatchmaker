using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.EmailSystem.Model;


namespace BlazorApp.EmailSystem.Services {

    public interface IMailService {
        Task SendEmailAsync(string ToEmail, string Subject, string HTMLBody);
    }
    
}