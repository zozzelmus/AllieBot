namespace AllieBot.Providers
{
    public class SlashCommandProvider
    {
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
