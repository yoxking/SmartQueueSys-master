using EntFrm.Business.BLL;
using EntFrm.Business.Model;
using EntFrm.Business.Model.Collections;
using EntFrm.Framework.Utility; 
using EntFrm.MainService.Entities;
using EntFrm.MainService.Models;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace EntFrm.MainService.Services
{
    public class SpeechService
    {
        private volatile static SpeechService _instance = null;
        private static readonly object lockHelper = new object();
        private AsynQueue<SpeechData> speechQueue;

        public static SpeechService CreateInstance()
        {
            if (_instance == null)
            {
                lock (lockHelper)
                {
                    if (_instance == null)
                        _instance = new SpeechService();

                }
            }
            return _instance;
        }

        private SpeechService()
        {
            speechQueue = new AsynQueue<SpeechData>();
            speechQueue.ProcessItemFunction += doSpeechText;
            speechQueue.ProcessException += doExpection; //new EventHandler<EventArgs<Exception>>(C);
        }


        private void doSpeechText(SpeechData data)
        {
            try
            {
                //if (!string.IsNullOrEmpty(data.PreMusic))
                //{
                //    doPlayMusic(data.PreMusic,data.VoiceVolume);
                //    //Thread.Sleep(1000);
                //}

                //CustomSpeech cs = new CustomSpeech();
                //cs.SpeakText(data.VoiceText, data.VoiceName, data.VoiceVolume, data.VoiceRate);

                MainFrame.DoSpeechText(data.VoiceText, data.VoiceName, data.VoiceVolume, data.VoiceRate); 
                Thread.Sleep(1000);
            }
            catch (Exception ex) { }
        }

        private void doExpection(object ex, EventArgs<Exception> args)
        {
        }

        /// <summary>
        /// 播放背景音乐
        /// </summary>
        /// <param name="sTicketFlowNo"></param>
        /// <param name="sCounterNo"></param>
        /// <returns></returns>
        private void doPlayMusic(string sMusicFile, int iVolume)
        {
            try
            {
                MediaPlayEx mplayer = new MediaPlayEx();
                mplayer.OpenMusic(sMusicFile, IntPtr.Zero);
                mplayer.PlayMusic();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 播放叫号语音 tts
        /// </summary>
        /// <param name="sTicketFlowNo"></param>
        /// <param name="sCounterNo"></param>
        /// <returns></returns>
        public bool doPlayVoice(string sCounterNo, string sPFlowNo, string sVoiceModel = "calling")
        {
            try
            {
                bool bResult = false;
                SpeechData speech = null;
                VoiceInfoBLL ttsBoss = new VoiceInfoBLL(IUserContext.GetConnStr(), IUserContext.GetAppCode()); //业务逻辑层实例
                CounterInfo counterInfo = IPublicHelper.GetCounterByNo(sCounterNo);

                if (counterInfo != null && speechQueue != null)
                {
                    string[] ttsNos = counterInfo.sVoiceStyleNos.Split(';');
                    string strSpeech = "";
                    VoiceInfo ttsInfo = null;

                    foreach (string ttsNo in ttsNos)
                    {
                        ttsInfo = ttsBoss.GetRecordByNo(ttsNo);
                        if (ttsInfo != null)
                        {
                            if (sVoiceModel.Equals("calling"))
                            {
                                strSpeech = ttsInfo.sFormatCalling;
                            }
                            else
                            {
                                strSpeech = ttsInfo.sFormatWaiting;
                            }
                            strSpeech = IPublicHelper.ReplaceVariables(strSpeech, sPFlowNo);

                            speech = new SpeechData();
                            speech.CounterNo = counterInfo.sCounterNo;
                            speech.VoiceName = ttsInfo.sVoice;
                            speech.VoiceRate = ttsInfo.iRate;
                            speech.VoiceVolume = ttsInfo.iVolume;
                            speech.VoiceText = strSpeech;
                            speech.PreMusic = ttsInfo.sPreMusic;
                            speech.PostMusic = ttsInfo.sPostMusic;

                            //插入语音播放队列
                            speechQueue.Enqueue(speech); 

                            bResult = true;
                        }
                    }
                }
                return bResult;
            }
            catch (Exception ex)
            {
                MainFrame.PrintMessage(DateTime.Now.ToString("[HH:mm:ss] ") + "播放语音出错，详细：" + ex.Message);
                return false;
            }
        }

        public void doPlayVoice(string sCounterNo, string sVoiceName, int iVoiceRate, int iVoiceVolume, string sText)
        {
            SpeechData speech = new SpeechData();
            speech.CounterNo = sCounterNo;
            speech.VoiceName = sVoiceName;
            speech.VoiceRate = iVoiceRate;
            speech.VoiceVolume = iVoiceVolume;
            speech.VoiceText = sText;
            speech.PreMusic = "";
            speech.PostMusic = "";

            //插入语音播放队列
            speechQueue.Enqueue(speech); 
        } 

    }
}
