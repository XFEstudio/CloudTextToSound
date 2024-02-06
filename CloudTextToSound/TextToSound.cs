using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CloudTextToSound
{
    /// <summary>
    /// 语音合成音色类型
    /// </summary>
    public enum VoiceTimbreType
    {
        /// <summary>
        /// ID：10510000 分类：阅读男声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智逍遥,

        /// <summary>
        /// ID：1001 分类：情感女声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智瑜,

        /// <summary>
        /// ID：1002 分类：通用女声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智聆,

        /// <summary>
        /// ID：1003 分类：客服女声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智美,

        /// <summary>
        /// ID：1004 分类：通用男声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智云,

        /// <summary>
        /// ID：1005 分类：通用女声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智莉,

        /// <summary>
        /// ID：1007 分类：客服女声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智娜,

        /// <summary>
        /// ID：1008 分类：客服女声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智琪,

        /// <summary>
        /// ID：1009 分类：知性女声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智芸,

        /// <summary>
        /// ID：1010 分类：通用男声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智华,

        /// <summary>
        /// ID：1017 分类：情感女声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智蓉,

        /// <summary>
        /// ID：1018 分类：情感男声 音色：标准音色 语言：中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智靖,

        /// <summary>
        /// ID：1050 分类：英文男声 音色：标准音色 语言：英文 清晰度：8k/16k 情感：中性
        /// </summary>
        WeJack,

        /// <summary>
        /// ID：1051 分类：英文女声 音色：标准音色 语言：英文 清晰度：8k/16k 情感：中性
        /// </summary>
        WeRose,

        /// <summary>
        /// ID：101006 分类：助手女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智言,

        /// <summary>
        /// ID：101011 分类：新闻女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智燕,

        /// <summary>
        /// ID：101012 分类：新闻女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智丹,

        /// <summary>
        /// ID：101013 分类：新闻男声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智辉,

        /// <summary>
        /// ID：101014 分类：新闻男声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智宁,

        /// <summary>
        /// ID：101015 分类：男童声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智萌,

        /// <summary>
        /// ID：101016 分类：女童声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智甜,

        /// <summary>
        /// ID：101019 分类：粤语女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智彤,

        /// <summary>
        /// ID：101020 分类：新闻男声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智刚,

        /// <summary>
        /// ID：101021 分类：新闻男声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智瑞,

        /// <summary>
        /// ID：101022 分类：新闻女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智虹,

        /// <summary>
        /// ID：101023 分类：聊天女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智萱,

        /// <summary>
        /// ID：101024 分类：聊天男声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智皓,

        /// <summary>
        /// ID：101025 分类：聊天女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智薇,

        /// <summary>
        /// ID：101026 分类：通用女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智希,

        /// <summary>
        /// ID：101027 分类：通用女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智梅,

        /// <summary>
        /// ID：101028 分类：通用女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智洁,

        /// <summary>
        /// ID：101029 分类：通用男声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智凯,

        /// <summary>
        /// ID：101030 分类：通用男声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智柯,

        /// <summary>
        /// ID：101031 分类：通用男声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智奎,

        /// <summary>
        /// ID：101032 分类：通用女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智芳,

        /// <summary>
        /// ID：101033 分类：客服女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智蓓,

        /// <summary>
        /// ID：101034 分类：通用女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智莲,

        /// <summary>
        /// ID：101035 分类：通用女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智依,

        /// <summary>
        /// ID：101040 分类：四川女声 音色：精品音色中文8k/16k 情感：中性
        /// </summary>
        智川,

        /// <summary>
        /// ID：101050 分类：英文男声 音色：精品音色英文8k/16k 情感：中性
        /// </summary>
        WeJack精品,

        /// <summary>
        /// ID：101051 分类：英文女声 音色：精品音色英文8k/16k 情感：中性
        /// </summary>
        WeRose精品,

        /// <summary>
        /// ID：101052 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智味,

        /// <summary>
        /// ID：101053 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智方,

        /// <summary>
        /// ID：101054 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智友,

        /// <summary>
        /// ID：101055 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智付,

        /// <summary>
        /// ID：101056 分类：东北男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智林,

        /// <summary>
        /// ID：301000 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小广,

        /// <summary>
        /// ID：301001 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小栋,

        /// <summary>
        /// ID：301002 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小海,

        /// <summary>
        /// ID：301003 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小霞,

        /// <summary>
        /// ID：301004 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小玲,

        /// <summary>
        /// ID：301005 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小章,

        /// <summary>
        /// ID：301006 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小峰,

        /// <summary>
        /// ID：301007 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小亮,

        /// <summary>
        /// ID：301008 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小博,

        /// <summary>
        /// ID：301009 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小芸,

        /// <summary>
        /// ID：301010 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小秋,

        /// <summary>
        /// ID：301011 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小芳,

        /// <summary>
        /// ID：301012 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小琴,

        /// <summary>
        /// ID：301013 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小康,

        /// <summary>
        /// ID：301014 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小辉,

        /// <summary>
        /// ID：301015 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小璐,

        /// <summary>
        /// ID：301016 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小阳,

        /// <summary>
        /// ID：301017 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小泉,

        /// <summary>
        /// ID：301018 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小昆,

        /// <summary>
        /// ID：301019 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小诚,

        /// <summary>
        /// ID：301020 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小岚,

        /// <summary>
        /// ID：301021 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小茹,

        /// <summary>
        /// ID：301022 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小蓉,

        /// <summary>
        /// ID：301023 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小燕,

        /// <summary>
        /// ID：301024 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小莲,

        /// <summary>
        /// ID：301025 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小武,

        /// <summary>
        /// ID：301026 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小雪,

        /// <summary>
        /// ID：301027 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小媛,

        /// <summary>
        /// ID：301028 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小娴,

        /// <summary>
        /// ID：301029 分类：通用男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性、高兴、生气、悲伤、恐惧、新闻、故事、广播、诗歌、客服
        /// </summary>
        爱小涛,
        /// <summary>
        /// ID：301030 分类：对话女声 音色：精品音色 中文 清晰度：8k/16k/24k 情感：中性
        /// </summary>
        爱小溪,

        /// <summary>
        /// ID：301031 分类：对话男声 音色：精品音色 中文 清晰度：8k/16k/24k 情感：中性、悲伤、生气、高兴、恐惧、震惊、撒娇、厌恶
        /// </summary>
        爱小树,

        /// <summary>
        /// ID：301032 分类：对话女声 音色：精品音色 中文 清晰度：8k/16k/24k 情感：中性、悲伤、生气、高兴、恐惧、震惊、撒娇、厌恶
        /// </summary>
        爱小荷,

        /// <summary>
        /// ID：301033 分类：对话女声 音色：精品音色 中文 清晰度：8k/16k/24k 情感：中性、悲伤、生气、高兴、恐惧、震惊、撒娇、厌恶
        /// </summary>
        爱小叶,

        /// <summary>
        /// ID：301034 分类：对话男声 音色：精品音色 中文 清晰度：8k/16k/24k 情感：中性、悲伤、生气、高兴、恐惧、震惊、撒娇、厌恶
        /// </summary>
        爱小杭,

        /// <summary>
        /// ID：301035 分类：对话女声 音色：精品音色 中文 清晰度：8k/16k/24k 情感：中性、悲伤、生气、高兴、恐惧、震惊、撒娇、厌恶
        /// </summary>
        爱小梅,

        /// <summary>
        /// ID：301036 分类：对话男声 音色：精品音色 中文 清晰度：8k/16k/24k 情感：中性，悲伤，生气，高兴，恐惧，震惊，撒娇，厌恶
        /// </summary>
        爱小柯,

        /// <summary>
        /// ID：301037 分类：对话女声 音色：精品音色 中文 清晰度：8k/16k/24k 情感：中性
        /// </summary>
        爱小静,

        /// <summary>
        /// ID：301038 分类：对话女声 音色：精品音色 中文 清晰度：8k/16k/24k 情感：中性，平静，兴奋，诗歌，悲伤，生气，恐惧，撒娇，厌恶
        /// </summary>
        爱小桃,

        /// <summary>
        /// ID：401000 分类：活力女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智妍,

        /// <summary>
        /// ID：401001 分类：温暖女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        温柔智萱,

        /// <summary>
        /// ID：401002 分类：活力男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智飞,

        /// <summary>
        /// ID：401003 分类：活力女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智悦,

        /// <summary>
        /// ID：401004 分类：成熟男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智海,

        /// <summary>
        /// ID：401005 分类：新闻男声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智凡,

        /// <summary>
        /// ID：401006 分类：导航女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智航,

        /// <summary>
        /// ID：401007 分类：广告女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智婷,

        /// <summary>
        /// ID：401008 分类：通用女声 音色：精品音色 中文 清晰度：8k/16k 情感：中性
        /// </summary>
        智霞
    }
    /// <summary>
    /// 音色枚举类型扩展
    /// </summary>
    public static class VoiceTimbreExtention
    {
        /// <summary>
        /// 获取音色ID
        /// </summary>
        /// <param name="voiceTimbreType"></param>
        /// <returns></returns>
        /// <exception cref="CloudTextToSoundException"></exception>
        public static int GetID(this VoiceTimbreType voiceTimbreType)
        {
            switch (voiceTimbreType)
            {
                case VoiceTimbreType.智逍遥:
                    return 10510000;
                case VoiceTimbreType.智瑜:
                    return 1001;
                case VoiceTimbreType.智聆:
                    return 1002;
                case VoiceTimbreType.智美:
                    return 1003;
                case VoiceTimbreType.智云:
                    return 1004;
                case VoiceTimbreType.智莉:
                    return 1005;
                case VoiceTimbreType.智娜:
                    return 1007;
                case VoiceTimbreType.智琪:
                    return 1008;
                case VoiceTimbreType.智芸:
                    return 1009;
                case VoiceTimbreType.智华:
                    return 1010;
                case VoiceTimbreType.智蓉:
                    return 1017;
                case VoiceTimbreType.智靖:
                    return 1018;
                case VoiceTimbreType.WeJack:
                    return 1050;
                case VoiceTimbreType.WeRose:
                    return 1051;
                case VoiceTimbreType.智言:
                    return 101006;
                case VoiceTimbreType.智燕:
                    return 101011;
                case VoiceTimbreType.智丹:
                    return 101012;
                case VoiceTimbreType.智辉:
                    return 101013;
                case VoiceTimbreType.智宁:
                    return 101014;
                case VoiceTimbreType.智萌:
                    return 101015;
                case VoiceTimbreType.智甜:
                    return 101016;
                case VoiceTimbreType.智彤:
                    return 101019;
                case VoiceTimbreType.智刚:
                    return 101020;
                case VoiceTimbreType.智瑞:
                    return 101021;
                case VoiceTimbreType.智虹:
                    return 101022;
                case VoiceTimbreType.智萱:
                    return 101023;
                case VoiceTimbreType.智皓:
                    return 101024;
                case VoiceTimbreType.智薇:
                    return 101025;
                case VoiceTimbreType.智希:
                    return 101026;
                case VoiceTimbreType.智梅:
                    return 101027;
                case VoiceTimbreType.智洁:
                    return 101028;
                case VoiceTimbreType.智凯:
                    return 101029;
                case VoiceTimbreType.智柯:
                    return 101030;
                case VoiceTimbreType.智奎:
                    return 101031;
                case VoiceTimbreType.智芳:
                    return 101032;
                case VoiceTimbreType.智蓓:
                    return 101033;
                case VoiceTimbreType.智莲:
                    return 101034;
                case VoiceTimbreType.智依:
                    return 101035;
                case VoiceTimbreType.智川:
                    return 101040;
                case VoiceTimbreType.WeJack精品:
                    return 101050;
                case VoiceTimbreType.WeRose精品:
                    return 101051;
                case VoiceTimbreType.智味:
                    return 101052;
                case VoiceTimbreType.智方:
                    return 101053;
                case VoiceTimbreType.智友:
                    return 101054;
                case VoiceTimbreType.智付:
                    return 101055;
                case VoiceTimbreType.智林:
                    return 101056;
                case VoiceTimbreType.爱小广:
                    return 301000;
                case VoiceTimbreType.爱小栋:
                    return 301001;
                case VoiceTimbreType.爱小海:
                    return 301002;
                case VoiceTimbreType.爱小霞:
                    return 301003;
                case VoiceTimbreType.爱小玲:
                    return 301004;
                case VoiceTimbreType.爱小章:
                    return 301005;
                case VoiceTimbreType.爱小峰:
                    return 301006;
                case VoiceTimbreType.爱小亮:
                    return 301007;
                case VoiceTimbreType.爱小博:
                    return 301008;
                case VoiceTimbreType.爱小芸:
                    return 301009;
                case VoiceTimbreType.爱小秋:
                    return 301010;
                case VoiceTimbreType.爱小芳:
                    return 301011;
                case VoiceTimbreType.爱小琴:
                    return 301012;
                case VoiceTimbreType.爱小康:
                    return 301013;
                case VoiceTimbreType.爱小辉:
                    return 301014;
                case VoiceTimbreType.爱小璐:
                    return 301015;
                case VoiceTimbreType.爱小阳:
                    return 301016;
                case VoiceTimbreType.爱小泉:
                    return 301017;
                case VoiceTimbreType.爱小昆:
                    return 301018;
                case VoiceTimbreType.爱小诚:
                    return 301019;
                case VoiceTimbreType.爱小岚:
                    return 301020;
                case VoiceTimbreType.爱小茹:
                    return 301021;
                case VoiceTimbreType.爱小蓉:
                    return 301022;
                case VoiceTimbreType.爱小燕:
                    return 301023;
                case VoiceTimbreType.爱小莲:
                    return 301024;
                case VoiceTimbreType.爱小武:
                    return 301025;
                case VoiceTimbreType.爱小雪:
                    return 301026;
                case VoiceTimbreType.爱小媛:
                    return 301027;
                case VoiceTimbreType.爱小娴:
                    return 301028;
                case VoiceTimbreType.爱小涛:
                    return 301029;
                case VoiceTimbreType.爱小溪:
                    return 301030;
                case VoiceTimbreType.爱小树:
                    return 301031;
                case VoiceTimbreType.爱小荷:
                    return 301032;
                case VoiceTimbreType.爱小叶:
                    return 301033;
                case VoiceTimbreType.爱小杭:
                    return 301034;
                case VoiceTimbreType.爱小梅:
                    return 301035;
                case VoiceTimbreType.爱小柯:
                    return 301036;
                case VoiceTimbreType.爱小静:
                    return 301037;
                case VoiceTimbreType.爱小桃:
                    return 301038;
                case VoiceTimbreType.智妍:
                    return 401000;
                case VoiceTimbreType.温柔智萱:
                    return 401001;
                case VoiceTimbreType.智飞:
                    return 401002;
                case VoiceTimbreType.智悦:
                    return 401003;
                case VoiceTimbreType.智海:
                    return 401004;
                case VoiceTimbreType.智凡:
                    return 401005;
                case VoiceTimbreType.智航:
                    return 401006;
                case VoiceTimbreType.智婷:
                    return 401007;
                case VoiceTimbreType.智霞:
                    return 401008;
                default:
                    throw new CloudTextToSoundException("未知的语音类型");
            }
        }
    }
    /// <summary>
    /// 语音返回的音频格式
    /// </summary>
    public enum CodecType
    {
        /// <summary>
        /// 返回多段含 opus 压缩分片音频（默认）
        /// </summary>
        opus,
        /// <summary>
        /// 返回二进制 pcm 音频
        /// </summary>
        pcm,
        /// <summary>
        /// 返回二进制 mp3 音频
        /// </summary>
        mp3
    }
    /// <summary>
    /// 音频设备类
    /// </summary>
    public class VoiceDevice
    {
        /// <summary>
        /// 获取字符串名称
        /// </summary>
        /// <returns>字符串名称</returns>
        public override string ToString()
        {
            return GetIndexAndName();
        }
        /// <summary>
        /// 当前设备的索引
        /// </summary>
        public int DeviceIndex { get; } = 0;
        /// <summary>
        /// 当前设备的名称
        /// </summary>
        public string DeviceName { get; } = string.Empty;
        /// <summary>
        /// 创建一个音频设备类
        /// </summary>
        /// <param name="DeviceIndex">音频设备的索引</param>
        /// <param name="DeviceName">音频设备的名称和描述</param>
        public VoiceDevice(int DeviceIndex, string DeviceName)
        {
            this.DeviceIndex = DeviceIndex;
            this.DeviceName = DeviceName;
        }
        /// <summary>
        /// 获取用于展示的索引+名称描述文本
        /// </summary>
        /// <returns>索引和名称的string</returns>
        public string GetIndexAndName()
        {
            return $"设备编号：[{DeviceIndex}]\n设备名称：{DeviceName}";
        }
    }
    /// <summary>
    /// 语音合成类
    /// </summary>
    public class TextToSound
    {
        private int AppId;
        private bool PlayAudioList = false;
        private bool FirstAudioPlayed = false;
        private string SecretId;
        private string SecretKey;
        private List<TTSDialog> dialogs = new List<TTSDialog>();
        /// <summary>
        /// 播放音频列表结束事件
        /// </summary>
        public event EventHandler AudioListPlayEnd;
        /// <summary>
        /// 初始化实时转写
        /// </summary>
        /// <param name="AppId">应用的APPID</param>
        /// <param name="SecretId">应用的SecretID</param>
        /// <param name="SecretKey">应用的SecertKey</param>
        /// <returns></returns>
        public void InitializTTS(int AppId, string SecretId, string SecretKey)
        {
            this.AppId = AppId;
            this.SecretId = SecretId;
            this.SecretKey = SecretKey;
        }
        /// <summary>
        /// 获取当前的音频输入设备列表
        /// </summary>
        /// <returns>音频设备列表</returns>
        public static List<VoiceDevice> GetVoiceOutputDevice()
        {
            var inputDeviceCount = WaveOut.DeviceCount;
            var DeviceList = new List<VoiceDevice>();
            for (int i = 0; i < inputDeviceCount; i++)
            {
                var capabilities = WaveOut.GetCapabilities(i);
                DeviceList.Add(new VoiceDevice(i, capabilities.ProductName));
            }
            return DeviceList;
        }
        /// <summary>
        /// 创建一个实时TTS的实例
        /// </summary>
        /// <param name="voiceTimbreType">音色类型</param>
        /// <returns>TTS对话</returns>
        public TTSDialog CreateDialog(VoiceTimbreType voiceTimbreType)
        {
            return new TTSDialog(AppId, SecretId, SecretKey, 600, 0, CodecType.pcm, voiceTimbreType);
        }
        /// <summary>
        /// 创建一个实时TTS的实例
        /// </summary>
        /// <param name="voiceDevice">音频设备</param>
        /// <param name="voiceTimbreType">音色类型</param>
        /// <returns>TTS对话</returns>
        public TTSDialog CreateDialog(VoiceDevice voiceDevice, VoiceTimbreType voiceTimbreType)
        {
            return new TTSDialog(AppId, SecretId, SecretKey, 600, voiceDevice.DeviceIndex, CodecType.pcm, voiceTimbreType);
        }
        /// <summary>
        /// 创建一个实时TTS的实例
        /// </summary>
        /// <param name="maxTime">最大时间</param>
        /// <param name="voiceTimbreType">音色类型</param>
        /// <returns>TTS对话</returns>
        public TTSDialog CreateDialog(int maxTime, VoiceTimbreType voiceTimbreType)
        {
            return new TTSDialog(AppId, SecretId, SecretKey, maxTime, 0, CodecType.pcm, voiceTimbreType);
        }
        /// <summary>
        /// 创建一个实时TTS的实例
        /// </summary>
        /// <param name="maxTime">最大时间</param>
        /// <param name="voiceDevice">音频设备</param>
        /// <param name="codecType">编码类型</param>
        /// <param name="voiceTimbreType">音色类型</param>
        /// <returns></returns>
        public TTSDialog CreateDialog(int maxTime, VoiceDevice voiceDevice, CodecType codecType, VoiceTimbreType voiceTimbreType)
        {
            return new TTSDialog(AppId, SecretId, SecretKey, maxTime, voiceDevice.DeviceIndex, codecType, voiceTimbreType);
        }
        /// <summary>
        /// 播放音频列表
        /// </summary>
        public void StartPlayAudioList()
        {
            if (!PlayAudioList)
            {
                PlayAudioList = true;
                Task.Run(() =>
                {
                    while (PlayAudioList)
                    {
                        if (dialogs.Count > 0)
                        {
                            if (!FirstAudioPlayed)
                            {
                                FirstAudioPlayed = true;
                            }
                            dialogs[0].StartTTS().Wait();
                            if (dialogs.Count > 0)
                            {
                                dialogs.RemoveAt(0);
                            }
                        }
                        else if (FirstAudioPlayed)
                        {
                            AudioListPlayEnd?.Invoke(this, new EventArgs());
                            FirstAudioPlayed = false;
                        }
                    }
                });
            }
        }
        /// <summary>
        /// 添加音频到播放列表
        /// </summary>
        /// <param name="dialog">TTS对话</param>
        public void AddAudioToPlayList(TTSDialog dialog)
        {
            dialogs.Add(dialog);
        }
        /// <summary>
        /// 移除指定的音频
        /// </summary>
        /// <param name="dialog"></param>
        public void RemoveAudioFromPlayList(TTSDialog dialog)
        {
            dialogs.Remove(dialog);
        }
        /// <summary>
        /// 移除指定位置的音频
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAudioFromPlayList(int index)
        {
            dialogs.RemoveAt(index);
        }
        /// <summary>
        /// 移除最后一个音频
        /// </summary>
        public void RemoveLastAudioFromPlayList()
        {
            dialogs.RemoveAt(dialogs.Count - 1);
        }
        /// <summary>
        /// 移除第一个音频
        /// </summary>
        public void RemoveFirstAudioFromPlayList()
        {
            dialogs.RemoveAt(0);
        }
        /// <summary>
        /// 清空音频播放列表
        /// </summary>
        public void ClearAudioPlayList()
        {
            dialogs.Clear();
        }
        /// <summary>
        /// 结束播放音频列表
        /// </summary>
        public void EndPlayAudioList()
        {
            PlayAudioList = false;
        }
    }
    /// <summary>
    /// TTS对话类
    /// </summary>
    public class TTSDialog
    {
        private int MaxTime = 600;
        private int DeviceNumber = 0;
        private int AppId;
        private string UUID = Guid.NewGuid().ToString();
        private string SecretId;
        private string SecretKey;
        private BufferedWaveProvider bufferedWaveProvider;
        private WaveOutEvent waveOut;
        private CodecType CodecType;
        private VoiceTimbreType VoiceTimbreType;
        private const string ParamAction = "TextToStreamAudioWS";
        /// <summary>
        /// TTS的文字
        /// </summary>
        public string Text { get; set; } = string.Empty;
        /// <summary>
        /// 说话速度
        /// </summary>
        public float Speed { get; set; } = 0;
        /// <summary>
        /// 音量
        /// </summary>
        public float Volume { get; set; } = 0;
        /// <summary>
        /// TTS接收到文本以及音频数据的事件
        /// </summary>
        public event EventHandler<TTSDialogEventArgs> OnTTSDialogReceived;
        /// <summary>
        /// 音频播放完成事件
        /// </summary>
        public event EventHandler AudioPlayFinished;
        /// <summary>
        /// 音频接收完成事件
        /// </summary>
        public event EventHandler AudioReceiveCompleted;
        /// <summary>
        /// 开始TTS
        /// </summary>
        /// <param name="TTSText">待转音频的文字</param>
        /// <param name="UseInnerAudio">是否使用本dll内置的音频</param>
        /// <param name="AutoPlayAudio">是否自动播放音频</param>
        /// <returns></returns>
        public async Task StartTTS(string TTSText = "", bool UseInnerAudio = true, bool AutoPlayAudio = true)
        {
            if (TTSText == string.Empty)
            {
                TTSText = Text;
            }
            TimeSpan tp = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);//设置当前的UNIX时间戳
            string originParamString = $"Action={ParamAction}&AppId={AppId}&Codec={CodecType}&EnableSubtitle=True&Expired={Convert.ToInt64(tp.TotalSeconds) + MaxTime}&ModelType=1&SampleRate=16000&SecretId={SecretId}&SessionId={UUID}&Speed={Speed}&Text={TTSText}&Timestamp={Convert.ToInt64(tp.TotalSeconds)}&VoiceType={VoiceTimbreType.GetID()}&Volume={Volume}";
            string originSignatureString = "GETtts.cloud.tencent.com/stream_ws?" + originParamString;
            string signartureString = HttpUtility.UrlEncode(AllMethod.GetEncodeSignature(SecretKey, originSignatureString));
            string finalParamString = originParamString.Replace(TTSText, HttpUtility.UrlEncode(TTSText)) + $"&Signature={signartureString}";
            using (var clien = new ClientWebSocket())
            {
                await clien.ConnectAsync(new Uri($"wss://tts.cloud.tencent.com/stream_ws?{finalParamString}"), CancellationToken.None);
                if (clien.State == WebSocketState.Open)
                {
                    var waveFormat = new WaveFormat(16000, 16, 1); // 16k PCM, 16 bits per sample, mono
                    waveOut = new WaveOutEvent();
                    bufferedWaveProvider = new BufferedWaveProvider(waveFormat);
                    bufferedWaveProvider.DiscardOnBufferOverflow = true;
                    waveOut.DeviceNumber = DeviceNumber;
                    waveOut.Init(bufferedWaveProvider);
                    if (AutoPlayAudio)
                    {
                        waveOut.Play();
                    }

                    while (clien.State == WebSocketState.Open)
                    {
                        byte[] buffer = new byte[1024];
                        WebSocketReceiveResult result = await clien.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        if (result.MessageType == WebSocketMessageType.Binary)
                        {
                            // 此处为16k的pcm音频数据
                            if (OnTTSDialogReceived != null)
                            {
                                OnTTSDialogReceived.Invoke(this, new TTSDialogEventArgs(null, buffer));
                            }
                            if (UseInnerAudio)
                            {
                                var bufferList = new List<byte>();
                                bufferList.AddRange(buffer.Take(result.Count));
                                while (!result.EndOfMessage)
                                {
                                    result = await clien.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                                    bufferList.AddRange(buffer.Take(result.Count));
                                }
                                var finalResult = bufferList.ToArray();
                                bufferedWaveProvider.BufferLength += finalResult.Length * finalResult.Length;
                                bufferedWaveProvider.AddSamples(finalResult, 0, finalResult.Length);
                            }
                        }
                        else if (result.MessageType == WebSocketMessageType.Text)
                        {
                            // 此处为json格式的字幕数据
                            string Subtitle = Encoding.UTF8.GetString(buffer, 0, result.Count);
                            // 循环读取直至消息结束
                            while (!result.EndOfMessage)
                            {
                                result = await clien.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                                Subtitle += Encoding.UTF8.GetString(buffer, 0, result.Count);
                            }
                            try
                            {
                                var backMessage = JsonConvert.DeserializeObject<TTSBackMessage>(Subtitle);
                                if (OnTTSDialogReceived != null)
                                {
                                    OnTTSDialogReceived.Invoke(this, new TTSDialogEventArgs(backMessage, null));
                                }
                                if (backMessage.final == 1)
                                {
                                    await clien.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None);
                                    if (AudioReceiveCompleted != null)
                                    {
                                        AudioReceiveCompleted.Invoke(this, new EventArgs());
                                    }
                                    while (bufferedWaveProvider.BufferedDuration != TimeSpan.Zero) { }
                                    if (AudioPlayFinished != null)
                                    {
                                        AudioPlayFinished.Invoke(this, new EventArgs());
                                    }
                                    waveOut.Stop();
                                    waveOut.Dispose();
                                    OnTTSDialogReceived = null;
                                    AudioPlayFinished = null;
                                    AudioReceiveCompleted = null;
                                    break;
                                }
                            }
                            catch (JsonException ex)
                            {
                                Console.WriteLine(Subtitle);
                                if (OnTTSDialogReceived != null)
                                {
                                    OnTTSDialogReceived.Invoke(this, new TTSDialogEventArgs(null, null, ex));
                                }
                                if (AudioReceiveCompleted != null)
                                {
                                    AudioReceiveCompleted.Invoke(this, new EventArgs());
                                }
                                await clien.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None);
                                while (bufferedWaveProvider.BufferedDuration != TimeSpan.Zero) { }
                                if (AudioPlayFinished != null)
                                {
                                    AudioPlayFinished.Invoke(this, new EventArgs());
                                }
                                waveOut.Stop();
                                waveOut.Dispose();
                                OnTTSDialogReceived = null;
                                AudioPlayFinished = null;
                                AudioReceiveCompleted = null;
                                break;
                            }
                        }
                    }

                }
                else
                {
                    Console.WriteLine("握手失败！状态为：" + clien.State);
                }
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            OnTTSDialogReceived = null;
            AudioPlayFinished = null;
            AudioReceiveCompleted = null;
            waveOut.Dispose();
            bufferedWaveProvider.ClearBuffer();
        }
        /// <summary>
        /// 播放音频
        /// </summary>
        public void Play()
        {
            while (waveOut == null) { }
            while (true)
            {
                try
                {
                    waveOut.Play();
                    break;
                }
                catch (Exception) { }
            }
        }
        /// <summary>
        /// 立即播放音频
        /// </summary>
        public void InstantPlay()
        {
            waveOut.Play();
        }
        /// <summary>
        /// 暂停音频
        /// </summary>
        public void Pause()
        {
            waveOut.Pause();
        }
        /// <summary>
        /// 停止音频
        /// </summary>
        public void Stop()
        {
            waveOut.Stop();
        }
        internal TTSDialog(int AppId, string SecretId, string SecretKey, int MaxTime, int DeviceNumber, CodecType codecType, VoiceTimbreType voiceTimbreType)
        {
            this.AppId = AppId;
            this.SecretId = SecretId;
            this.SecretKey = SecretKey;
            this.MaxTime = MaxTime;
            this.DeviceNumber = DeviceNumber;
            this.CodecType = codecType;
            this.VoiceTimbreType = voiceTimbreType;
        }
    }
    /// <summary>
    /// TTS对话事件参数
    /// </summary>
    public class TTSDialogEventArgs : EventArgs
    {
        /// <summary>
        /// TTS返回消息
        /// </summary>
        public TTSBackMessage backMessage { get; private set; }
        /// <summary>
        /// 是否有返回消息
        /// </summary>
        public bool HasBackMessage { get { return backMessage != null; } }
        /// <summary>
        /// 是否有音频数据 
        /// </summary>
        public bool HasBuffer { get { return buffer != null; } }
        /// <summary>
        /// TTS返回音频数据
        /// </summary>
        public byte[] buffer { get; private set; }
        /// <summary>
        /// 异常
        /// </summary>
        public Exception exception { get; private set; }
        internal TTSDialogEventArgs(TTSBackMessage backMessage, byte[] buffer)
        {
            this.backMessage = backMessage;
            this.buffer = buffer;
        }
        internal TTSDialogEventArgs(TTSBackMessage backMessage, byte[] buffer, Exception exception)
        {
            this.backMessage = backMessage;
            this.buffer = buffer;
            this.exception = exception;
        }
    }
    /// <summary>
    /// TTS返回消息
    /// </summary>
    public class TTSBackMessage
    {
        /// <summary>
        /// 返回值，0为成功
        /// </summary>
        public int code { get; private set; }
        /// <summary>
        /// 返回消息，success为成功
        /// </summary>
        public string message { get; private set; }
        /// <summary>
        /// 该次请求的session_id
        /// </summary>
        public string session_id { get; private set; }
        /// <summary>
        /// 该次请求的request_id
        /// </summary>
        public string request_id { get; private set; }
        /// <summary>
        /// 该次请求的message_id
        /// </summary>
        public string message_id { get; private set; }
        /// <summary>
        /// 请求的返回结果
        /// </summary>
        public TTSResult result { get; private set; }
        /// <summary>
        /// 是否为最后一条消息，1为最后一条
        /// </summary>
        public int final { get; private set; }
        /// <summary>
        /// TTS返回消息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="session_id"></param>
        /// <param name="request_id"></param>
        /// <param name="message_id"></param>
        /// <param name="result"></param>
        /// <param name="final"></param>
        [JsonConstructor]
        public TTSBackMessage(int code, string message, string session_id, string request_id, string message_id, TTSResult result, int final)
        {
            this.code = code;
            this.message = message;
            this.session_id = session_id;
            this.request_id = request_id;
            this.message_id = message_id;
            this.result = result;
            this.final = final;
        }
    }
    /// <summary>
    /// TTS结果
    /// </summary>
    public class TTSResult
    {
        /// <summary>
        /// TTS结构体
        /// </summary>
        public struct TTSResultStruct
        {
            /// <summary>
            /// 该字的内容
            /// </summary>
            public string Text { get; set; }
            /// <summary>
            /// 该字在整个音频流中的起始时间
            /// </summary>
            public int BeginTime { get; set; }
            /// <summary>
            /// 该字在整个音频流中的结束时间
            /// </summary>
            public int EndTime { get; set; }
            /// <summary>
            /// 该字在整个文本中的开始位置，从0开始
            /// </summary>
            public int BeginIndex { get; set; }
            /// <summary>
            /// 该字在整个文本中的结束位置，从0开始
            /// </summary>
            public int EndIndex { get; set; }
            /// <summary>
            /// 该字的音素（注意：此字段可能返回 null，表示取不到有效值）
            /// </summary>
            public string Phoneme { get; set; }
        }
        /// <summary>
        /// TTS结构
        /// </summary>
        public TTSResultStruct[] subtitles { get; private set; }
        /// <summary>
        /// TTS结果
        /// </summary>
        /// <param name="subtitles"></param>
        [JsonConstructor]
        public TTSResult(TTSResultStruct[] subtitles)
        {
            this.subtitles = subtitles;
        }
    }
}
