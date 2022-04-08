using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore.Repositories
{
	public class PerguntasRepository : IPerguntasRepository
	{
		public readonly IMongoClient client;

		public PerguntasRepository(IMongoClient _client)
		{
			client = _client;
		}

		public void GravaPergunta(string pergunta)
		{
			var database = client.GetDatabase("BotEmulator");
			var data = database.GetCollection<BsonDocument>("Perguntas");

			var document = new BsonDocument(){
						{"pergunta", pergunta }
					};

			data.InsertOne(document);
		}
	}
}
