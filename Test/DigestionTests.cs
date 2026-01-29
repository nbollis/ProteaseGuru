using Engine;
using NUnit.Framework;
using Proteomics.ProteolyticDigestion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tasks;
using UsefulProteomicsDatabases;

namespace Test
{
    [TestFixture]
    public class DigestionTests
    {
        [Test]
        public static void SingleDatabase()
        {
            string subFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, @"DigestionTest");
            Directory.CreateDirectory(subFolder);

            string databasePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Databases", "TestDatabase_1.fasta");
            DbForDigestion database = new DbForDigestion(databasePath);

            Parameters param = new Parameters();
            param.MinPeptideLengthAllowed = 1;
            param.MaxPeptideLengthAllowed = 100;
            param.NumberOfMissedCleavagesAllowed = 0;
            param.TreatModifiedPeptidesAsDifferent = false;
            param.ProteasesForDigestion.Add(ProteaseDictionary.Dictionary["trypsin (cleave before proline)"]);
            param.OutputFolder = subFolder;

            DigestionTask digestion = new DigestionTask();
            digestion.DigestionParameters = param;
            var digestionResults = digestion.RunSpecific(subFolder, new List<DbForDigestion>() { database });
            Assert.That(digestionResults.PeptideByFile.Count, Is.EqualTo(1));
            Assert.That(digestionResults.PeptideByFile.Values.Count, Is.EqualTo(1));
            Assert.That(digestionResults.PeptideByFile[database.FileName][param.ProteasesForDigestion.First().Name].Count, Is.EqualTo(2));
            foreach (var entry in digestionResults.PeptideByFile[database.FileName][param.ProteasesForDigestion.First().Name])
            {
                if (entry.Key.Accession == "testProtein_1")
                {
                    Assert.That(entry.Value.Count, Is.EqualTo(28));

                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].Unique, Is.False);
                    Assert.That(entry.Value[0].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[0].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].Unique, Is.False);
                    Assert.That(entry.Value[1].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[1].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[2].BaseSequence, Is.EqualTo("QGHYAVGAFNTNNLEWTR"));
                    Assert.That(entry.Value[2].Unique, Is.True);
                    Assert.That(entry.Value[2].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[2].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[3].BaseSequence, Is.EqualTo("AILK"));
                    Assert.That(entry.Value[3].Unique, Is.True);
                    Assert.That(entry.Value[3].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[3].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[5].BaseSequence, Is.EqualTo("AAQEK"));
                    Assert.That(entry.Value[5].Unique, Is.False);
                    Assert.That(entry.Value[5].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[5].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[6].BaseSequence, Is.EqualTo("NTPVLIQVSMGAAK"));
                    Assert.That(entry.Value[6].Unique, Is.False);
                    Assert.That(entry.Value[6].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[6].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[7].BaseSequence, Is.EqualTo("YMGDYK"));
                    Assert.That(entry.Value[7].Unique, Is.False);
                    Assert.That(entry.Value[7].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[7].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[8].BaseSequence, Is.EqualTo("LVK"));
                    Assert.That(entry.Value[8].Unique, Is.False);
                    Assert.That(entry.Value[8].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[8].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[9].BaseSequence, Is.EqualTo("TLVEEEMR"));
                    Assert.That(entry.Value[9].Unique, Is.True);
                    Assert.That(entry.Value[9].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[9].SeqOnlyInThisDb, Is.True);
                }
                else if (entry.Key.Accession == "testProtein_2")
                {
                    Assert.That(entry.Value.Count, Is.EqualTo(29));

                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].Unique, Is.False);
                    Assert.That(entry.Value[0].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[0].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].Unique, Is.False);
                    Assert.That(entry.Value[1].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[1].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[2].BaseSequence, Is.EqualTo("AAQEK"));
                    Assert.That(entry.Value[2].Unique, Is.False);
                    Assert.That(entry.Value[2].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[2].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[23].BaseSequence, Is.EqualTo("QGHPPGAFNTNNLEWTR"));
                    Assert.That(entry.Value[23].Unique, Is.True);
                    Assert.That(entry.Value[23].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[23].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[24].BaseSequence, Is.EqualTo("AIVK"));
                    Assert.That(entry.Value[24].Unique, Is.True);
                    Assert.That(entry.Value[24].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[24].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[3].BaseSequence, Is.EqualTo("NTPVLIQVSMGAAK"));
                    Assert.That(entry.Value[3].Unique, Is.False);
                    Assert.That(entry.Value[3].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[3].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[4].BaseSequence, Is.EqualTo("YMGDYK"));
                    Assert.That(entry.Value[4].Unique, Is.False);
                    Assert.That(entry.Value[4].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[4].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[5].BaseSequence, Is.EqualTo("LVK"));
                    Assert.That(entry.Value[5].Unique, Is.False);
                    Assert.That(entry.Value[5].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[5].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[25].BaseSequence, Is.EqualTo("TLVEPPMR"));
                    Assert.That(entry.Value[25].Unique, Is.True);
                    Assert.That(entry.Value[25].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[25].SeqOnlyInThisDb, Is.True);
                }
            }

            Directory.Delete(subFolder, true);
        }

        [Test]
        public static void MultipleDatabases()
        {
            string subFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, @"DigestionTest");
            Directory.CreateDirectory(subFolder);

            string databasePath1 = Path.Combine(TestContext.CurrentContext.TestDirectory, "Databases", "TestDatabase_1.fasta");
            DbForDigestion database1 = new DbForDigestion(databasePath1);

            string databasePath2 = Path.Combine(TestContext.CurrentContext.TestDirectory, "Databases", "TestDatabase_2.fasta");
            DbForDigestion database2 = new DbForDigestion(databasePath2);

            string databasePath3 = Path.Combine(TestContext.CurrentContext.TestDirectory, "Databases", "TestDatabase_3.fasta");
            DbForDigestion database3 = new DbForDigestion(databasePath3);

            Parameters param = new Parameters();
            param.MinPeptideLengthAllowed = 1;
            param.MaxPeptideLengthAllowed = 100;
            param.NumberOfMissedCleavagesAllowed = 0;
            param.TreatModifiedPeptidesAsDifferent = false;
            param.ProteasesForDigestion.Add(ProteaseDictionary.Dictionary["trypsin (cleave before proline)"]);
            param.OutputFolder = subFolder;

            DigestionTask digestion = new DigestionTask();
            digestion.DigestionParameters = param;
            var digestionResults = digestion.RunSpecific(subFolder, new List<DbForDigestion>() { database1, database2, database3 });
            Assert.That(digestionResults.PeptideByFile.Count, Is.EqualTo(3));
            Assert.That(digestionResults.PeptideByFile.Values.Count, Is.EqualTo(3));
            Assert.That(digestionResults.PeptideByFile[database1.FileName][param.ProteasesForDigestion.First().Name].Count, Is.EqualTo(2));
            Assert.That(digestionResults.PeptideByFile[database2.FileName][param.ProteasesForDigestion.First().Name].Count, Is.EqualTo(2));
            Assert.That(digestionResults.PeptideByFile[database3.FileName][param.ProteasesForDigestion.First().Name].Count, Is.EqualTo(2));

            foreach (var entry in digestionResults.PeptideByFile[database1.FileName][param.ProteasesForDigestion.First().Name])
            {
                if (entry.Key.Accession == "testProtein_1")
                {
                    Assert.That(entry.Value.Count, Is.EqualTo(28));

                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].Unique, Is.False);
                    Assert.That(entry.Value[0].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[0].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].Unique, Is.False);
                    Assert.That(entry.Value[1].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[1].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[2].BaseSequence, Is.EqualTo("QGHYAVGAFNTNNLEWTR"));
                    Assert.That(entry.Value[2].Unique, Is.True);
                    Assert.That(entry.Value[2].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[2].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[3].BaseSequence, Is.EqualTo("AILK"));
                    Assert.That(entry.Value[3].Unique, Is.True);
                    Assert.That(entry.Value[3].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[3].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[5].BaseSequence, Is.EqualTo("AAQEK"));
                    Assert.That(entry.Value[5].Unique, Is.False);
                    Assert.That(entry.Value[5].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[5].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[6].BaseSequence, Is.EqualTo("NTPVLIQVSMGAAK"));
                    Assert.That(entry.Value[6].Unique, Is.False);
                    Assert.That(entry.Value[6].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[6].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[7].BaseSequence, Is.EqualTo("YMGDYK"));
                    Assert.That(entry.Value[7].Unique, Is.False);
                    Assert.That(entry.Value[7].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[7].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[8].BaseSequence, Is.EqualTo("LVK"));
                    Assert.That(entry.Value[8].Unique, Is.False);
                    Assert.That(entry.Value[8].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[8].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[9].BaseSequence, Is.EqualTo("TLVEEEMR"));
                    Assert.That(entry.Value[9].Unique, Is.True);
                    Assert.That(entry.Value[9].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[9].SeqOnlyInThisDb, Is.False);
                }
                else if (entry.Key.Accession == "testProtein_2")
                {
                    Assert.That(entry.Value.Count, Is.EqualTo(29));

                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].Unique, Is.False);
                    Assert.That(entry.Value[0].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[0].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].Unique, Is.False);
                    Assert.That(entry.Value[1].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[1].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[2].BaseSequence, Is.EqualTo("AAQEK"));
                    Assert.That(entry.Value[2].Unique, Is.False);
                    Assert.That(entry.Value[2].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[2].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[23].BaseSequence, Is.EqualTo("QGHPPGAFNTNNLEWTR"));
                    Assert.That(entry.Value[23].Unique, Is.True);
                    Assert.That(entry.Value[23].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[23].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[24].BaseSequence, Is.EqualTo("AIVK"));
                    Assert.That(entry.Value[24].Unique, Is.True);
                    Assert.That(entry.Value[24].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[24].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[3].BaseSequence, Is.EqualTo("NTPVLIQVSMGAAK"));
                    Assert.That(entry.Value[3].Unique, Is.False);
                    Assert.That(entry.Value[3].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[3].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[4].BaseSequence, Is.EqualTo("YMGDYK"));
                    Assert.That(entry.Value[4].Unique, Is.False);
                    Assert.That(entry.Value[4].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[4].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[5].BaseSequence, Is.EqualTo("LVK"));
                    Assert.That(entry.Value[5].Unique, Is.False);
                    Assert.That(entry.Value[5].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[5].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[25].BaseSequence, Is.EqualTo("TLVEPPMR"));
                    Assert.That(entry.Value[25].Unique, Is.True);
                    Assert.That(entry.Value[25].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[25].SeqOnlyInThisDb, Is.False);
                }
            }

            foreach (var entry in digestionResults.PeptideByFile[database2.FileName][param.ProteasesForDigestion.First().Name])
            {
                if (entry.Key.Accession == "testProtein_A")
                {
                    Assert.That(entry.Value.Count, Is.EqualTo(28));

                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].Unique, Is.False);
                    Assert.That(entry.Value[0].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[0].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].Unique, Is.False);
                    Assert.That(entry.Value[1].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[1].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[2].BaseSequence, Is.EqualTo("QGHYAVGAFNTNNLEWTR"));
                    Assert.That(entry.Value[2].Unique, Is.True);
                    Assert.That(entry.Value[2].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[2].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[3].BaseSequence, Is.EqualTo("AILK"));
                    Assert.That(entry.Value[3].Unique, Is.False);
                    Assert.That(entry.Value[3].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[3].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[4].BaseSequence, Is.EqualTo("AAQEK"));
                    Assert.That(entry.Value[4].Unique, Is.False);
                    Assert.That(entry.Value[4].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[4].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[5].BaseSequence, Is.EqualTo("NTPVLIQVSMGAAK"));
                    Assert.That(entry.Value[5].Unique, Is.False);
                    Assert.That(entry.Value[5].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[5].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[6].BaseSequence, Is.EqualTo("YMGDYK"));
                    Assert.That(entry.Value[6].Unique, Is.False);
                    Assert.That(entry.Value[6].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[6].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[7].BaseSequence, Is.EqualTo("LVK"));
                    Assert.That(entry.Value[7].Unique, Is.False);
                    Assert.That(entry.Value[7].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[7].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[8].BaseSequence, Is.EqualTo("TLVEEEMR"));
                    Assert.That(entry.Value[8].Unique, Is.True);
                    Assert.That(entry.Value[8].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[8].SeqOnlyInThisDb, Is.False);
                }
                else if (entry.Key.Accession == "testProtein_B")
                {
                    Assert.That(entry.Value.Count, Is.EqualTo(29));

                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].Unique, Is.False);
                    Assert.That(entry.Value[0].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[0].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].Unique, Is.False);
                    Assert.That(entry.Value[1].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[1].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[2].BaseSequence, Is.EqualTo("AILK"));
                    Assert.That(entry.Value[2].Unique, Is.False);
                    Assert.That(entry.Value[2].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[2].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[3].BaseSequence, Is.EqualTo("AAQEK"));
                    Assert.That(entry.Value[3].Unique, Is.False);
                    Assert.That(entry.Value[3].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[3].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[25].BaseSequence, Is.EqualTo("QGHPPGAFNTNNLEWTR"));
                    Assert.That(entry.Value[25].Unique, Is.True);
                    Assert.That(entry.Value[25].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[25].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[4].BaseSequence, Is.EqualTo("NTPVLIQVSMGAAK"));
                    Assert.That(entry.Value[4].Unique, Is.False);
                    Assert.That(entry.Value[4].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[4].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[5].BaseSequence, Is.EqualTo("YMGDYK"));
                    Assert.That(entry.Value[5].Unique, Is.False);
                    Assert.That(entry.Value[5].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[5].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[6].BaseSequence, Is.EqualTo("LVK"));
                    Assert.That(entry.Value[6].Unique, Is.False);
                    Assert.That(entry.Value[6].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[6].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[26].BaseSequence, Is.EqualTo("TLVEPPMR"));
                    Assert.That(entry.Value[26].Unique, Is.True);
                    Assert.That(entry.Value[26].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[26].SeqOnlyInThisDb, Is.False);
                }
            }

            foreach (var entry in digestionResults.PeptideByFile[database3.FileName][param.ProteasesForDigestion.First().Name])
            {
                if (entry.Key.Accession == "testProtein_one")
                {
                    Assert.That(entry.Value.Count, Is.EqualTo(28));

                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].Unique, Is.True);
                    Assert.That(entry.Value[0].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[0].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].Unique, Is.True);
                    Assert.That(entry.Value[1].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[1].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[2].BaseSequence, Is.EqualTo("MGHAVVGAFNTNNLEWTR"));
                    Assert.That(entry.Value[2].Unique, Is.True);
                    Assert.That(entry.Value[2].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[2].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[3].BaseSequence, Is.EqualTo("AILK"));
                    Assert.That(entry.Value[3].Unique, Is.False);
                    Assert.That(entry.Value[3].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[3].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[4].BaseSequence, Is.EqualTo("AAQEK"));
                    Assert.That(entry.Value[4].Unique, Is.False);
                    Assert.That(entry.Value[4].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[4].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[5].BaseSequence, Is.EqualTo("NTPVLIQVSMGAAK"));
                    Assert.That(entry.Value[5].Unique, Is.True);
                    Assert.That(entry.Value[5].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[5].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[6].BaseSequence, Is.EqualTo("YMGDYK"));
                    Assert.That(entry.Value[6].Unique, Is.False);
                    Assert.That(entry.Value[6].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[6].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[7].BaseSequence, Is.EqualTo("LVK"));
                    Assert.That(entry.Value[7].Unique, Is.False);
                    Assert.That(entry.Value[7].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[7].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[8].BaseSequence, Is.EqualTo("TLVEEEMR"));
                    Assert.That(entry.Value[8].Unique, Is.True);
                    Assert.That(entry.Value[8].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[8].SeqOnlyInThisDb, Is.False);
                }
                else if (entry.Key.Accession == "testProtein_two")
                {
                    Assert.That(entry.Value.Count, Is.EqualTo(29));

                    Assert.That(entry.Value[19].BaseSequence, Is.EqualTo("MSFVNGNEIFTQER"));
                    Assert.That(entry.Value[19].Unique, Is.True);
                    Assert.That(entry.Value[19].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[19].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[21].BaseSequence, Is.EqualTo("QGHPPGAFNTNNLEWTR"));
                    Assert.That(entry.Value[21].Unique, Is.True);
                    Assert.That(entry.Value[21].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[21].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("AILK"));
                    Assert.That(entry.Value[0].Unique, Is.False);
                    Assert.That(entry.Value[0].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[0].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("AAQEK"));
                    Assert.That(entry.Value[1].Unique, Is.False);
                    Assert.That(entry.Value[1].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[1].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[22].BaseSequence, Is.EqualTo("NTPVLIQVSMGAAVR"));
                    Assert.That(entry.Value[22].Unique, Is.True);
                    Assert.That(entry.Value[22].UniqueAllDbs, Is.True);
                    Assert.That(entry.Value[22].SeqOnlyInThisDb, Is.True);

                    Assert.That(entry.Value[2].BaseSequence, Is.EqualTo("YMGDYK"));
                    Assert.That(entry.Value[2].Unique, Is.False);
                    Assert.That(entry.Value[2].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[2].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[3].BaseSequence, Is.EqualTo("LVK"));
                    Assert.That(entry.Value[3].Unique, Is.False);
                    Assert.That(entry.Value[3].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[3].SeqOnlyInThisDb, Is.False);

                    Assert.That(entry.Value[23].BaseSequence, Is.EqualTo("TLVEPPMR"));
                    Assert.That(entry.Value[23].Unique, Is.True);
                    Assert.That(entry.Value[23].UniqueAllDbs, Is.False);
                    Assert.That(entry.Value[23].SeqOnlyInThisDb, Is.False);
                }
            }

            Directory.Delete(subFolder, true);
        }

        [Test]
        public static void ProteaseModTest()
        {
            string subFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, @"DigestionTest");
            Directory.CreateDirectory(subFolder);

            string databasePath1 = Path.Combine(TestContext.CurrentContext.TestDirectory, "Databases", "ProteaseModTest.fasta");
            DbForDigestion database1 = new DbForDigestion(databasePath1);

            var protDic = ProteaseDictionary.LoadProteaseDictionary(Path.Combine(GlobalVariables.DataDir, @"ProteolyticDigestion", @"proteases.tsv"), GlobalVariables.ProteaseMods);

            Parameters param = new Parameters();
            param.MinPeptideLengthAllowed = 1;
            param.MaxPeptideLengthAllowed = 100;
            param.NumberOfMissedCleavagesAllowed = 0;
            param.TreatModifiedPeptidesAsDifferent = false;
            param.ProteasesForDigestion.Add(protDic["CNBr"]);
            param.OutputFolder = subFolder;

            DigestionTask digestion = new DigestionTask();
            digestion.DigestionParameters = param;
            var digestionResults = digestion.RunSpecific(subFolder, new List<DbForDigestion>() { database1 });

            foreach (var entry in digestionResults.PeptideByFile[database1.FileName][param.ProteasesForDigestion.First().Name])
            {
                var peptides = entry.Value;
                Assert.That(peptides.Count(), Is.EqualTo(2));
                Assert.That(peptides[0].FullSequence, Is.Not.EqualTo(peptides[1].FullSequence));
                Assert.That(peptides[0].MolecularWeight, Is.EqualTo(882.39707781799996));
                Assert.That(peptides[1].MolecularWeight, Is.EqualTo(930.400449121));
            }
        }

        [Test]
        public static void InitiatorMethionineTest()
        {
            string subFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, @"DigestionTest");
            Directory.CreateDirectory(subFolder);

            string databasePath1 = Path.Combine(TestContext.CurrentContext.TestDirectory, "Databases", "TestDatabase_1.fasta");
            DbForDigestion database1 = new DbForDigestion(databasePath1);

            string databasePath2 = Path.Combine(TestContext.CurrentContext.TestDirectory, "Databases", "TestDatabase_2.fasta");
            DbForDigestion database2 = new DbForDigestion(databasePath2);

            string databasePath3 = Path.Combine(TestContext.CurrentContext.TestDirectory, "Databases", "TestDatabase_3.fasta");
            DbForDigestion database3 = new DbForDigestion(databasePath3);

            Parameters param = new Parameters();
            param.MinPeptideLengthAllowed = 1;
            param.MaxPeptideLengthAllowed = 100;
            param.NumberOfMissedCleavagesAllowed = 0;
            param.TreatModifiedPeptidesAsDifferent = false;
            param.ProteasesForDigestion.Add(ProteaseDictionary.Dictionary["trypsin (cleave before proline)"]);
            param.OutputFolder = subFolder;

            DigestionTask digestion = new DigestionTask();
            digestion.DigestionParameters = param;
            var digestionResults = digestion.RunSpecific(subFolder, new List<DbForDigestion>() { database1, database2, database3 });
            Assert.That(digestionResults.PeptideByFile.Count, Is.EqualTo(3));
            Assert.That(digestionResults.PeptideByFile.Values.Count, Is.EqualTo(3));
            Assert.That(digestionResults.PeptideByFile[database1.FileName][param.ProteasesForDigestion.First().Name].Count, Is.EqualTo(2));
            Assert.That(digestionResults.PeptideByFile[database2.FileName][param.ProteasesForDigestion.First().Name].Count, Is.EqualTo(2));
            Assert.That(digestionResults.PeptideByFile[database3.FileName][param.ProteasesForDigestion.First().Name].Count, Is.EqualTo(2));

            foreach (var entry in digestionResults.PeptideByFile[database1.FileName][param.ProteasesForDigestion.First().Name])
            {
                if (entry.Key.Accession == "testProtein_1")
                {
                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].PreviousAA, Is.EqualTo('-'));
                    Assert.That(entry.Value[0].StartResidue, Is.EqualTo(1));

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].PreviousAA, Is.EqualTo('M'));
                    Assert.That(entry.Value[1].StartResidue, Is.EqualTo(2));
                }
                else if (entry.Key.Accession == "testProtein_2")
                {
                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].PreviousAA, Is.EqualTo('-'));
                    Assert.That(entry.Value[0].StartResidue, Is.EqualTo(1));

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].PreviousAA, Is.EqualTo('M'));
                    Assert.That(entry.Value[1].StartResidue, Is.EqualTo(2));
                }
            }

            foreach (var entry in digestionResults.PeptideByFile[database2.FileName][param.ProteasesForDigestion.First().Name])
            {
                if (entry.Key.Accession == "testProtein_A")
                {
                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].PreviousAA, Is.EqualTo('-'));
                    Assert.That(entry.Value[0].StartResidue, Is.EqualTo(1));

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].PreviousAA, Is.EqualTo('M'));
                    Assert.That(entry.Value[1].StartResidue, Is.EqualTo(2));
                }
                else if (entry.Key.Accession == "testProtein_B")
                {
                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].PreviousAA, Is.EqualTo('-'));
                    Assert.That(entry.Value[0].StartResidue, Is.EqualTo(1));

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].PreviousAA, Is.EqualTo('M'));
                    Assert.That(entry.Value[1].StartResidue, Is.EqualTo(2));
                }
            }

            foreach (var entry in digestionResults.PeptideByFile[database3.FileName][param.ProteasesForDigestion.First().Name])
            {
                if (entry.Key.Accession == "testProtein_one")
                {
                    Assert.That(entry.Value[0].BaseSequence, Is.EqualTo("MSFVNGNEIFTAAR"));
                    Assert.That(entry.Value[0].PreviousAA, Is.EqualTo('-'));
                    Assert.That(entry.Value[0].StartResidue, Is.EqualTo(1));

                    Assert.That(entry.Value[1].BaseSequence, Is.EqualTo("SFVNGNEIFTAAR"));
                    Assert.That(entry.Value[1].PreviousAA, Is.EqualTo('M'));
                    Assert.That(entry.Value[1].StartResidue, Is.EqualTo(2));
                }
                else if (entry.Key.Accession == "testProtein_two")
                {
                    Assert.That(entry.Value[19].BaseSequence, Is.EqualTo("MSFVNGNEIFTQER"));
                    Assert.That(entry.Value[19].PreviousAA, Is.EqualTo('-'));
                    Assert.That(entry.Value[19].StartResidue, Is.EqualTo(1));

                    Assert.That(entry.Value[20].BaseSequence, Is.EqualTo("SFVNGNEIFTQER"));
                    Assert.That(entry.Value[20].PreviousAA, Is.EqualTo('M'));
                    Assert.That(entry.Value[20].StartResidue, Is.EqualTo(2));
                }
            }

            Directory.Delete(subFolder, true);
        }
    }
}