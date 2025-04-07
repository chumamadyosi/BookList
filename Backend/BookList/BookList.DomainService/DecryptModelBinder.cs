using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookList.DomainService
{
    public class DecryptModelBinder<T> : IModelBinder
    {
        private readonly IEncryptionService _encryptionService;

        public DecryptModelBinder(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;
            string encryptedData;

            // Read the encrypted data from the request body
            using (var reader = new StreamReader(request.Body))
            {
                encryptedData = await reader.ReadToEndAsync();
            }

            if (string.IsNullOrEmpty(encryptedData))
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            // Decrypt the encrypted data
            var decryptedData = _encryptionService.Decrypt(encryptedData);

            if (decryptedData == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            // Deserialize the decrypted data into the target model
            var model = JsonSerializer.Deserialize<T>(decryptedData);

            if (model == null)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return;
            }

            // Set the model result to the decrypted and deserialized data
            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}

