namespace AllieBot.Providers
{
    public class SlashCommandProvider
    {
        /// <summary>
        /// Used to handle the Dota2OpenAPI rank return value which at the time of implementation comes back as two digits: 53
        /// </summary>
        /// <param name="rank">Two digit int representing the rank of a player in Dota 2</param>
        /// <returns>String of rank as displays in game.</returns>
        public string ParseRank(int rank)
        {
            //handle rank
            var rankArr = rank.ToString();
            var medalNum = Convert.ToInt32(rankArr.Substring(0, 1));
            var tier = rankArr.ToString().Substring(1, 1);

            //tiers
            List<string> tiers = new List<string>() { "Herald", "Guardian", "Crusader", "Archon", "Legend", "Anchient", "Divine", "Immortal" };
            var rankMsg = tiers[medalNum];

            return (string)$"Rank: {rankMsg} {tier}";
        }
    }
}
