using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

namespace TestApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("This is the EABA character creation project");
            string currentAction = "Menu";
            string response = "";
            Character currentCharacter = null;

            while (currentAction != "Exit")
            {
                if (currentAction == "Menu")
                {
                    if (currentCharacter == null)
                    {
                        Console.WriteLine("");
                        Console.WriteLine("No character selected!");
                        Console.WriteLine("");
                        Console.WriteLine("1. New Character");
                        Console.WriteLine("2. Load Character");
                        Console.WriteLine("3. Exit");
                        Console.WriteLine("");
                        response = Console.ReadLine();
                        if (response == "1")
                        {
                            currentCharacter = newChararacter();
                        }
                        else if (response == "2")
                        {
                            Console.WriteLine("Not implemented yet!");
                        }
                        else if (response == "3")
                        {
                            currentAction = "Exit";
                        }
                        else
                        {
                            Console.WriteLine("Invalid option.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("");
                        Console.WriteLine("Character " + currentCharacter.name + " selected!");
                        Console.WriteLine("");
                        Console.WriteLine("1. New Character");
                        Console.WriteLine("2. Load Character");
                        Console.WriteLine("3. Exit");
                        Console.WriteLine("4. Show character");
                        Console.WriteLine("5. Save character");
                        Console.WriteLine("");
                        response = Console.ReadLine();
                        if (response == "1")
                        {
                            currentCharacter = new Character();
                        }
                        else if (response == "2")
                        {
                            Console.WriteLine("Not implemented yet!");
                        }
                        else if (response == "3")
                        {
                            currentAction = "Exit";
                        }
                        else if (response == "4")
                        {
                            currentAction = "Show";
                        }
                        else if (response == "5")
                        {
                            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "SaveCharacterTest.txt")))
                            {
                                await outputFile.WriteLineAsync("This is the save file for character: " + currentCharacter.name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid option.");
                        }
                    }
                }
                else if (currentAction == "Show")
                {
                    Console.WriteLine("");
                    Console.WriteLine("+----------------------------------------------+");
                    Console.WriteLine("|Name: " + currentCharacter.name);
                    Console.WriteLine("|Age: " + currentCharacter.age);
                    Console.WriteLine("+----------------------------------------------+");
                    Console.WriteLine("|Attribute Points: " + currentCharacter.getAttributePointsSpent() + "/" + currentCharacter.baseAttributePoints);
                    Console.WriteLine("|Skill Points: 0/" + currentCharacter.baseSkillPoints);
                    Console.WriteLine("+----------------------------------------------+");
                    Console.WriteLine("|Strength: " + currentCharacter.attributes["Strength"] + " | Agility: " + currentCharacter.attributes["Agility"] + " | Health: " + currentCharacter.attributes["Health"]);
                    Console.WriteLine("|Will: " + currentCharacter.attributes["Will"] + " | Awareness: " + currentCharacter.attributes["Awareness"] + " | Fate:" + currentCharacter.attributes["Fate"]);
                    Console.WriteLine("+----------------------------------------------+");
                    int iteration = 0;
                    string writeThis = "";
                    foreach (KeyValuePair<string, subAttribute> subAttribute in currentCharacter.subAttributes)
                    {
                        if (iteration == 0)
                        {
                            writeThis = "| " + subAttribute.Key + ": " + ReturnDiceValue(subAttribute.Value.value + currentCharacter.attributes[subAttribute.Value.keyedAttribute]);
                            iteration++;
                        }
                        else if (iteration == 1)
                        {
                            writeThis += " | " + subAttribute.Key + ": " + ReturnDiceValue(subAttribute.Value.value + currentCharacter.attributes[subAttribute.Value.keyedAttribute]);
                            iteration++;
                        }
                        else if (iteration == 2)
                        {
                            writeThis += " | " + subAttribute.Key + ": " + ReturnDiceValue(subAttribute.Value.value + currentCharacter.attributes[subAttribute.Value.keyedAttribute]);
                            Console.WriteLine(writeThis);
                            iteration = 0;
                        }
                    }
                    Console.WriteLine("+----------------------------------------------+");
                    iteration = 0;
                    int foreachLength = currentCharacter.skills.Count;
                    int totalIterations = 1;
                    string finalResult = "";
                    foreach (KeyValuePair<string, int> skill in currentCharacter.skills)
                    {
                        if (totalIterations == foreachLength)
                        {
                            if (iteration == 0)
                            {
                                finalResult = "| " + skill.Key + ": " + skill.Value;
                                Console.WriteLine(finalResult);
                            }
                            else
                            {
                                finalResult += " | " + skill.Key + ": " + skill.Value;
                                Console.WriteLine(finalResult);
                            }
                        }
                        else
                        {
                            if (iteration == 0)
                            {
                                Console.WriteLine("");
                                finalResult = "| " + skill.Key + ": " + skill.Value;
                                iteration++;
                                totalIterations++;
                            }
                            else if (iteration == 1)
                            {
                                finalResult += " | " + skill.Key + ": " + skill.Value;
                                iteration++;
                                totalIterations++;
                            }
                            else
                            {
                                finalResult += " | " + skill.Key + ": " + skill.Value;
                                Console.WriteLine(finalResult);
                                iteration = 0;
                                totalIterations++;
                            }
                        }
                    }
                    Console.WriteLine("+----------------------------------------------+");
                    Console.WriteLine("");
                    Console.WriteLine("1. New Character");
                    Console.WriteLine("2. Load Character");
                    Console.WriteLine("3. Exit");
                    Console.WriteLine("4. Adjust Attribute");
                    Console.WriteLine("5. Adjust Subattribute");
                    Console.WriteLine("6. Adjust Skill");
                    Console.WriteLine("7. Add Skill");
                    Console.WriteLine("");
                    response = Console.ReadLine();
                    if (response == "1")
                    {
                        currentCharacter = new Character();
                    }
                    else if (response == "2")
                    {
                        Console.WriteLine("Not implemented yet!");
                    }
                    else if (response == "3")
                    {
                        currentAction = "Exit";
                    }
                    else if (response == "4")
                    {
                        Console.WriteLine("What attribute?");
                        Console.WriteLine("");
                        Console.WriteLine("+----------------------------------------------+");
                        iteration = 0;
                        string finalString = "";
                        foreach (KeyValuePair<string, int> attribute in currentCharacter.attributes)
                        {
                            if (iteration == 0)
                            {
                                finalString = "| " + attribute.Key;
                                iteration++;
                            }
                            else if (iteration == 1)
                            {
                                finalString += " | " + attribute.Key;
                                iteration++;
                            }
                            else if (iteration == 2)
                            {
                                finalString += " | " + attribute.Key;
                                Console.WriteLine(finalString);
                                iteration = 0;
                            }
                        }
                        Console.WriteLine("+----------------------------------------------+");
                        ChangeAttribute(currentCharacter, Console.ReadLine());
                    }
                    else if (response == "5")
                    {
                        Console.WriteLine("What subattribute?");
                        Console.WriteLine("");
                        Console.WriteLine("+----------------------------------------------+");
                        iteration = 0;
                        string finalString = "";
                        foreach (KeyValuePair<string, subAttribute> subAtt in currentCharacter.subAttributes)
                        {
                            if (iteration == 0)
                            {
                                finalString = "| " + subAtt.Key;
                                iteration++;
                            }
                            else if (iteration == 1)
                            {
                                finalString += " | " + subAtt.Key;
                                iteration++;
                            }
                            else if (iteration == 2)
                            {
                                finalString += " | " + subAtt.Key;
                                Console.WriteLine(finalString);
                                iteration = 0;
                            }
                        }
                        Console.WriteLine("+----------------------------------------------+");
                        ChangeSubAttribute(currentCharacter, Console.ReadLine());
                    }
                    else if (response == "6")
                    {
                        if(currentCharacter.skills.Count == 0)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("No Skills!");
                            Console.WriteLine("");
                        }
                        else
                        {
                            Console.WriteLine("+----------------------------------------------+");
                            iteration = 0;
                            foreachLength = currentCharacter.skills.Count;
                            totalIterations = 1;
                            finalResult = "";
                            foreach (KeyValuePair<string, int> skill in currentCharacter.skills)
                            {
                                if(totalIterations == foreachLength)
                                {
                                    if(iteration == 0)
                                    {
                                        Console.WriteLine("");
                                        finalResult = "| " + skill.Key + ": " + skill.Value;
                                        Console.WriteLine(finalResult);
                                    }
                                    else
                                    {
                                        finalResult += " | " + skill.Key + ": " + skill.Value;
                                        Console.WriteLine(finalResult);
                                    }
                                }
                                else
                                {
                                    if (iteration == 0)
                                    {
                                        Console.WriteLine("");
                                        finalResult = "| " + skill.Key + ": " + skill.Value;
                                        iteration++;
                                        totalIterations++;
                                    }
                                    else if (iteration == 1)
                                    {
                                        finalResult += " | " + skill.Key + ": " + skill.Value;
                                        iteration++;
                                        totalIterations++;
                                    }
                                    else
                                    {
                                        finalResult += " | " + skill.Key + ": " + skill.Value;
                                        Console.WriteLine(finalResult);
                                        iteration = 0;
                                        totalIterations++;
                                    }
                                }
                            }
                            Console.WriteLine("+----------------------------------------------+");
                            Console.WriteLine("");
                            Console.WriteLine("What skill to adjust?");
                            Console.WriteLine("");
                            string reponse = Console.ReadLine();
                            if (reponse != null)
                            {
                                ChangeSkill(currentCharacter, reponse);
                            }
                            else
                            {
                                Console.WriteLine("Skill doesn't exist!");
                            }
                        }
                    }
                    else if (response == "7")
                    {
                        Console.WriteLine("");
                        Console.WriteLine("What skill do you want to add?");
                        Console.WriteLine("");
                        string nameResponse = Console.ReadLine();
                        if (nameResponse != null)
                        {
                            string skillName = nameResponse;
                            Console.WriteLine("");
                            Console.WriteLine("What level should it be?");
                            Console.WriteLine("");
                            string levelResponse = Console.ReadLine();
                            int skillLevel = 0;
                            if (levelResponse != null && int.TryParse(levelResponse, out skillLevel))
                            {
                                currentCharacter.skills.Add(skillName, skillLevel);
                                Console.WriteLine("Skill " + skillName + " at level: " + currentCharacter.skills[skillName] + " successfully added!");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid option.");
                    }
                }
            }
            Exit();
        }

        public static void ChangeAttribute(Character character, string name)
        {
            if (character.attributes.ContainsKey(name))
            {
                Console.WriteLine("");
                Console.WriteLine("Input value:");
                Console.WriteLine("");
                string input = Console.ReadLine();
                int value = 0;
                if (input != null && int.TryParse(input, out value))
                {
                    character.attributes[name] = value;
                    Console.WriteLine(name + " changed to: " + value);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Incorrect value!");
                    Console.WriteLine("");
                    Console.ReadKey();
                }
                
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Incorrect value!");
                Console.WriteLine("");
                Console.ReadKey();
            }
        }

        public static void ChangeSubAttribute(Character character, string name)
        {
            if (character.subAttributes.ContainsKey(name))
            {
                Console.WriteLine("");
                Console.WriteLine("Input value:");
                Console.WriteLine("");
                string input = Console.ReadLine();
                int value = 0;
                if (input != null && int.TryParse(input, out value))
                {
                    character.subAttributes[name].value = value;
                    Console.WriteLine(name + " changed to: " + value);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Incorrect value!");
                    Console.WriteLine("");
                    Console.ReadKey();
                }

            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Incorrect value!");
                Console.WriteLine("");
                Console.ReadKey();
            }
        }

        public static void ChangeSkill(Character character, string name)
        {
            if (character.skills.ContainsKey(name))
            {
                Console.WriteLine("");
                Console.WriteLine("Input value:");
                Console.WriteLine("");
                string input = Console.ReadLine();
                int value = 0;
                if (input != null && int.TryParse(input, out value))
                {
                    character.skills[name] = value;
                    Console.WriteLine(name + " changed to: " + value);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Incorrect value!");
                    Console.WriteLine("");
                    Console.ReadKey();
                }

            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Incorrect value!");
                Console.WriteLine("");
                Console.ReadKey();
            }
        }

        public static Character newChararacter()
        {
            Character freshCharacter = new Character();
            freshCharacter.skills.Add("Brawling", 3);
            freshCharacter.skills.Add("Wrestling", 2);
            freshCharacter.skills.Add("Fishing", 1);

            freshCharacter.attributes.Add("Strength", 6);
            freshCharacter.attributes.Add("Agility", 6);
            freshCharacter.attributes.Add("Health", 6);
            freshCharacter.attributes.Add("Awareness", 6);
            freshCharacter.attributes.Add("Will", 6);
            freshCharacter.attributes.Add("Fate", 6);

            freshCharacter.subAttributes.Add("Strike", new subAttribute(-3, "Strength"));
            freshCharacter.subAttributes.Add("Carry", new subAttribute(0, "Strength"));
            freshCharacter.subAttributes.Add("StrThrow", new subAttribute(-3, "Strength"));
            freshCharacter.subAttributes.Add("Fight", new subAttribute(0, "Agility"));
            freshCharacter.subAttributes.Add("Balance", new subAttribute(0, "Agility"));
            freshCharacter.subAttributes.Add("AgiThrow", new subAttribute(0, "Agility"));
            freshCharacter.subAttributes.Add("Stamina", new subAttribute(0, "Health"));
            freshCharacter.subAttributes.Add("Speed", new subAttribute(0, "Health"));
            freshCharacter.subAttributes.Add("Recovery", new subAttribute(0, "Health"));
            freshCharacter.subAttributes.Add("Think", new subAttribute(0, "Awareness"));
            freshCharacter.subAttributes.Add("Sight", new subAttribute(0, "Awareness"));
            freshCharacter.subAttributes.Add("Hearing", new subAttribute(0, "Awareness"));
            freshCharacter.subAttributes.Add("Charm", new subAttribute(0, "Will"));
            freshCharacter.subAttributes.Add("Resist", new subAttribute(0, "Will"));
            freshCharacter.subAttributes.Add("Toughness", new subAttribute(0, "Will"));
            freshCharacter.subAttributes.Add("Luck", new subAttribute(0, "Fate"));
            freshCharacter.subAttributes.Add("Power", new subAttribute(0, "Fate"));
            freshCharacter.subAttributes.Add("Shield", new subAttribute(0, "Fate"));

            return freshCharacter;
        }

        static string ReturnDiceValue (int numberValue)
        {
            string finalString = "";
            double dividedNumberValue = (double)numberValue / 3;
            int diceValue = (int)Math.Truncate(dividedNumberValue);
            double bonusValue = dividedNumberValue - Math.Truncate(dividedNumberValue);
            int finalBonusValue;
            if (bonusValue == 0)
            {
                finalBonusValue = 0;
            }
            else if (bonusValue > 0.5)
            {
                finalBonusValue = 2;
            }
            else
            {
                finalBonusValue = 1;
            }
            finalString += diceValue;
            finalString += "d+";
            finalString += finalBonusValue;

            return finalString;
        }

        static void Exit()
        {
            return;
        }

        public class Character
        {
            public string name = "Not Named";
            public int age = 0;
            public int baseAttributePoints = 30;
            public int baseSkillPoints = 10;
            public int basePowerPoints = 0;

            public int getAttributePointsSpent()
            {
                int finalValue = 0;
                foreach(KeyValuePair<string, int> attribute in attributes)
                {
                    finalValue += attribute.Value;
                }
                return finalValue;
            }

            public Dictionary<string, int> skills = new Dictionary<string, int> ();
            public Dictionary<string, int> attributes = new Dictionary<string, int> ();
            public Dictionary<string, subAttribute> subAttributes = new Dictionary<string, subAttribute>();

            
        }
        public class subAttribute(int val = 0, string key = "")
        {
            public int value = val;
            public string keyedAttribute = key;
        }
    }
}
