using ESESMT.Domain.Entities;
using ESESMT.Infra.Data.Context;
using System;

namespace ESESMT.Application.UnitTests
{
    public static class DbContextExtensions
    {
        public static void Seed(this ApplicationDbContext dbContext)
        {
            var checklistType1 = new ChecklistType() {
                Id = 1,
                Name = "Tipo 1",
                IsActive = true
            };
            var checklistType2 = new ChecklistType()
            {
                Id = 2,
                Name = "Tipo 2",
                IsActive = true
            };
            var checklistType3 = new ChecklistType()
            {
                Id = 3,
                Name = "Tipo 3",
                IsActive = true
            };
            //var locale2 = new Locale(2, "Brasil", "Espírito Santo", "Abrangência Estadual ES");
            //var locale3 = new Locale(3, "Brasil", "Goiás", "Catalão");
            //var locale4 = new Locale(4, "Brasil", "Maranhão", "Açailândia");
            //var locale5 = new Locale(5, "Brasil", "Mato Grosso do Sul", "Campo Grande");
            dbContext.ChecklistTypes.Add(checklistType1);
            dbContext.ChecklistTypes.Add(checklistType2);
            dbContext.ChecklistTypes.Add(checklistType3);
            //dbContext.Locales.Add(locale4);
            //dbContext.Locales.Add(locale5);

            //var sourceChannel1 = new SourceChannel(1, "SRD");
            //var sourceChannel2 = new SourceChannel(2, "FALE CONOSCO");
            //var sourceChannel3 = new SourceChannel(3, "ALÔ FERROVIA");
            //dbContext.SourceChannels.Add(sourceChannel1);
            //dbContext.SourceChannels.Add(sourceChannel2);
            //dbContext.SourceChannels.Add(sourceChannel3);

            //var topic1 = new Topic(1, "Trem de Passageiro / Turístico e Logística");
            //var topic2 = new Topic(2, "Meio Ambiente");
            //var topic3 = new Topic(3, "Assistência Humanitária");
            //var topic4 = new Topic(4, "Outros[Antigo]");
            //var topic5 = new Topic(5, "Recursos Humanos");
            //dbContext.Topics.Add(topic1);
            //dbContext.Topics.Add(topic2);
            //dbContext.Topics.Add(topic3);
            //dbContext.Topics.Add(topic4);
            //dbContext.Topics.Add(topic5);

            //var subTopic1 = new SubTopic(1, "SubTópico 1", topic1);
            //var subTopic2 = new SubTopic(2, "SubTópico 2", topic2);
            //var subTopic3 = new SubTopic(3, "SubTópico 3", topic3);
            //var subTopic4 = new SubTopic(4, "SubTópico 4", topic4);
            //var subTopic5 = new SubTopic(5, "SubTópico 5", topic5);
            //dbContext.SubTopics.Add(subTopic1);
            //dbContext.SubTopics.Add(subTopic2);
            //dbContext.SubTopics.Add(subTopic3);
            //dbContext.SubTopics.Add(subTopic4);
            //dbContext.SubTopics.Add(subTopic5);

            //var type1 = new ManifestationType(1, "ELOGIO");
            //var type2 = new ManifestationType(2, "SUGESTÃO");
            //var type3 = new ManifestationType(3, "INFORMAÇÃO/DÚVIDA");
            //var type4 = new ManifestationType(4, "SOLICITAÇÃO");
            //var type5 = new ManifestationType(5, "RECLAMAÇÃO");
            //var type6 = new ManifestationType(6, "DENÚNCIA");
            //dbContext.ManifestationTypes.Add(type1);
            //dbContext.ManifestationTypes.Add(type2);
            //dbContext.ManifestationTypes.Add(type3);
            //dbContext.ManifestationTypes.Add(type4);
            //dbContext.ManifestationTypes.Add(type5);
            //dbContext.ManifestationTypes.Add(type6);

            //var status1 = new ManifestationStatus(1, "RESPONDIDA");
            //var status2 = new ManifestationStatus(2, "EM ANÁLISE");
            //var status3 = new ManifestationStatus(3, "EM ATENDIMENTO");
            //var status4 = new ManifestationStatus(4, "ATENDIDA");
            //var status5 = new ManifestationStatus(5, "NEGADA");
            //dbContext.ManifestationStatuses.Add(status1);
            //dbContext.ManifestationStatuses.Add(status2);
            //dbContext.ManifestationStatuses.Add(status3);
            //dbContext.ManifestationStatuses.Add(status4);
            //dbContext.ManifestationStatuses.Add(status5);

            //var manifestation1 = new Manifestation(1, "Teste 1", "teste1@teste1.com", "(11) 11111-1111",
            //    "111.111.111-11", "decrição do teste 1", "111111111", null, DateTime.Now.AddDays(-11),
            //    DateTime.Now.AddDays(11), null, null, null, "1", DateTime.Now.AddDays(-11),
            //    DateTime.Now.AddDays(-11), locale1, type1, topic1, subTopic1, status1, sourceChannel1, null);
            //var manifestation2 = new Manifestation(2, "Teste 2", "teste2@teste2.com", "(22) 22222-2222",
            //    "222.222.222-22", "decrição do teste 2", "222222222", null, DateTime.Now.AddDays(-22),
            //    DateTime.Now.AddDays(22), null, null, null, "2", DateTime.Now.AddDays(-22),
            //    DateTime.Now.AddDays(-22), locale2, type2, topic2, subTopic2, status2, sourceChannel2, null);
            //var manifestation3 = new Manifestation(3, "Teste 3", "teste3@teste3.com", "(33) 33333-3333",
            //    "333.333.333-33", "decrição do teste 3", "333333333", null, DateTime.Now.AddDays(-33),
            //    DateTime.Now.AddDays(33), null, null, null, "3", DateTime.Now.AddDays(-33),
            //    DateTime.Now.AddDays(-33), locale3, type3, topic3, subTopic3, status3, sourceChannel3, null);
            //var manifestation4 = new Manifestation(4, "Teste 4", "teste4@teste4.com", "(44) 44444-4444",
            //    "444.444.444-44", "decrição do teste 4", "444444444", null, DateTime.Now.AddDays(-44),
            //    DateTime.Now.AddDays(44), null, null, null, "4", DateTime.Now.AddDays(-44),
            //    DateTime.Now.AddDays(-44), locale4, type4, topic4, subTopic4, status4, sourceChannel1, null);
            //var manifestation5 = new Manifestation(5, "Teste 5", "teste5@teste5.com", "(55) 55555-5555",
            //    "555.555.555-55", "decrição do teste 5", "555555555", null, DateTime.Now.AddDays(-55),
            //    DateTime.Now.AddDays(55), null, null, null, "5", DateTime.Now.AddDays(-55),
            //    DateTime.Now.AddDays(-55), locale5, type5, topic5, subTopic5, status5, sourceChannel2, null);
            //var manifestation6 = new Manifestation(6, "Teste 6", "teste6@teste6.com", "(66) 66666-6666",
            //    "666.666.666-66", "decrição do teste 6", "666666666", null, DateTime.Now.AddDays(-66),
            //    DateTime.Now.AddDays(66), null, null, null, "6", DateTime.Now.AddDays(-66),
            //    DateTime.Now.AddDays(-66), locale5, type5, topic5, subTopic5, status5, sourceChannel3, null);
            //dbContext.Manifestations.Add(manifestation1);
            //dbContext.Manifestations.Add(manifestation2);
            //dbContext.Manifestations.Add(manifestation3);
            //dbContext.Manifestations.Add(manifestation4);
            //dbContext.Manifestations.Add(manifestation5);
            //dbContext.Manifestations.Add(manifestation6);

            dbContext.SaveChanges();
        }
    }
}
