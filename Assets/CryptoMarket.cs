using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CryptoMarket : MonoBehaviour {
	public KMBombModule module;
	public new KMAudio audio;
	public KMSelectable view;
	public KMSelectable invest;
	public KMSelectable[] cryptos;
	public TextMesh[] cryptoScreens;
	public TextMesh[] screens;
	public TextMesh moneyScreen;
	public KMSelectable[] UP;
	public KMSelectable[] DOWN;
	public Transform[] arrows;
	public AudioClip[] SFX;
	private int[] Q4Prices;
	private string[][] QChanges;
	private Color[][] QColors;
	private int[] ans;
	private bool flag;
	private int[] sub = new int[4];
	private static int moduleCounterID = 1;
	private int moduleID;
	private string[] nameList = 
	{
		"BTRS","CD","CW","IND","KP","KN","MZ","MEM","MC","P",
		"PORT","S","B","VG","WOF","WSEQ","W","COF","PK","SMP",
		"EMJM","M","LIO","SW","2B","AG","WS","CL","2FA","FLB",
		"MNS","AN","FER","LST","RKP","CCK","MMT","OQB","FMN","LK",
		"AS","RPH","LO","AV","CT","MYSQ","TTK","CPK","PB","SFS",
		"TS","CH","CY","TTKS","3DMZ","MITM","SYS","NUMP","SSTS","LN",
		"APB","PR","CACI","RE","SKS","MCC","PP","MU","GP","TTT",
		"MF","WTM","SSH","FTL","FS","BU","BAL","ET","SEAS","RPS",
		"SQB","HMZ","BITM","COSQ","ADJ","3RDB","SV","WOS","BKB","SSCR",
		"MAH","CB","BTS","SPW","TXF","WP","DO","CAPC","CO","LCY",
		"HTTP","COM","RYT","OC","NEUT","WD","CQ","CR","RQB","FZBZ",
		"CLK","LEDE","BWO","EW","FM","MSP","Z","BLED","BVD","POO",
		"IC","HTD","SCW","YZ","XR","ENIN","QR","BMSH","RNG","COMC",
		"MMC","MMS","GL","BC","MAMZ","CSW","PW","MTC","CGOL","GOL",
		"NNG","RTB","SET","COG","PTG","SMEM","SYCY","HT","MULW","EXPW",
		"CRCL","BR","MFA","FK","FLG","TZ","PMZ","PKR","SYC","PY",
		"STH","BSEQ","AGB","VI","JBX","ID","BK","BLMZ","MT","MK",
		"FBK","MSH","MOCI","RDT","LEDG","SI","IOS","SWN","WM","HR",
		"SR","BLA","PX","ECD","EUT","RPB","LG","RC","FSL","PI",
		"STW","FE","LOG","LU","TW","CODC","GM","SN","PLCI","TG",
		"CG","NUM","SLO","MN","NIND","QB","DRDR","TX","JV","DR",
		"GNUM","MBTB","X","LOB","TCD","TPCD","SSND","SSIG","GCAL","SY",
		"SSHK","CXKP","LZ","SWY","TR","GC","CLD","USA","BIT","TK",
		"BH","LS","SSTR","MW","MZS","MSK","TSM","TNC","ANUM","BSL",
		"DCO","EQ","MFG","DET","PQB","KYW","STL","CS","SSAM","DRG",
		"UNSQ","FL","3DT","SYNC","TSW","REVM","MANO","SKK","WSPG","MH",
		"TNS","BNDC","BO","HMEM","SIG","CMD","BMZ","SNK","QN","SP",
		"CFB","COMD","BSE","LIS","SK","BJ","PTM","AC","PLB","TD",
		"JOL","TWD","DVSQ","COND","IN","VV","BLKB","CPH","CDN","CCDN",
		"EM","CMZ","IK","RT","101","PT","SDB","MJ","KU","RD",
		"MOD","NN","CNC","F","TRI","SU","CHP","HO","FN","HI",
		"NMB","SCR","SSPN","CDO","TBCC","CBX","STF","LBR","COMT","SB",
		"FJBX","SKW","HOV","BPZ","FMZ","BGC","DM","HW","RCT","SSPK",
		"DISQ","DOM","KT","FC","NUMS","AY","CJ","FRP","SS","VASQ",
		"SSQW","ZN","MMEM","UANG","BTD","QM","DESQ","FTEX","FTX","SNB",
		"HMP","DETO","ATC","S125","MI","WE","LM","PIR","APBO","SSOD",
		"FGS","SSEQ","HSEQ","SSRM","UFCI","MSQ","COI","GV","LNR","PC",
		"WDG","HX","PLU","GSEQ","MCM","EF","MMZ","TSQ","FT","DICI",
		"BEA","D","GS","STP","LQB","MM2","PU","ST","GMEM","QZBZ",
		"WV","HQB","SE","7W","SEM","COK","TL","PN","NR","4CM",
		"AA","AP","DS","GID","HCO","SNP","CC","BS","VX","OOO",
		"MZM","TRIB","EQNX","MZQB","GH","AH","RA","FSI","SSTP","MB",
		"TQZ","BIW","DD","MODM","RW","SST","TRBS","CRPW","SGL","BLK",
		"BB","IT","TMO","AM","GARR","RARR","EE","EV","YARR","FTA",
		"OK","BARR","STN","HNUM","OARR","UOK","ROK","BG","FTD","MIOK",
		"MX","PARR","BOK","DMK","7DS","UQB","SYCO","RCK","DOMT","CHCD",
		"DOK","RTP","BD","PG","TOK","C10S","10S","BKP","CAL","DE",
		"MOPO","PCT","TE","QW","ATCB","SSS","CST","FEN","LD","CDR",
		"PRC","FDR","TCT","BTB","VICI","LA","OF","IA","TH","SNL",
		"MMV","BA","RNUM","SSQ","COT","AARR","BSTK","BW","DARR","CACY",
		"PD","VR","CACY","FUN","NPN","AFCY","PICY","FPT","PLCY","JUCY",
		"AB","FP","ORG","JA","BI","HICY","ULCY","CP","MTM","BBK",
		"SOF","FMW","WAY","SSLC","CRCY","SLS","WN","BAT","MS","RP",
		"HU","RCI","AMIB","FMEM","DB","CMS","OCI","NFM","TVAB","UOCI",
		"TV","1M","MDK","TIE","BBG","FRU","RU","FTN","LC","ML",
		"GK","GCI","KKKP","YCI","RGBM","BCI","LSYM","FML","KPL","UM",
		"HRD","FRGB","ICI","VCI","CHC","COA","EB","TOH","KPC","KJ",
		"UST","GD","TC","NNM","8PG","COMZ","WCI","ACI","KCI","HL",
		"LOV","CN","DNUM","HS","IG","CBMZ","INT","JG","CEPC","SPB",
		"MEMB","THW","OS","7C4","LT","NAT","NEU","SH","PYG","UCI",
		"CNM","OMO","BSTP","LOS","UCN","HG","14","15ML","3X3","ACBF",
		"BZFZ","EGG","FIN","TA","NVS","UARR","DIDI","MCSW","TSIG","HBN",
		"PCS","LOC","CRU","CODI","ED","PRE","NON","FO","BIG","64",
		"CKP","MAT","BPG","SF","GLG","BTK","SCDI","KW","TWLB","SOA",
		"DRCI","BF","BZGI","EL","HND","PAT","ROSQ","BX","TSTV","RCL",
		"ASA","CONB","SMM","HNS","SYT","APBR","MIC","WDS","DMDR","LPMZ",
		"V","AMSG","ALI","SIL","DG","UNC","PWG","BCR","GW","ABZ",
		"RABZ","GKP","LB","1KW","5LW","SOK","HV","BLU","RED","DIB",
		"PNCT","MISQ","SPT","NCD","NCW","NKP","NMZ","NM","NMEM","XP",
		"NS","NB","NWS","NWOF","NW","DG2F","SEQ","VCR","WOR","QTN",
		"ASEQ","OSU","SMZ","AAP","PHT","RV","SRT","PTL","SG","CATC",
		"MCCI","QA","FTC","TSS","ETT","CGK","RSW","1DMZ","RPN","SFK",
		"XPN","F7SD","FI","NVG","SS2","MFN","RGR","SIMG","MCP","MCSV",
		"NUMW","SFST","FCG","RPSJ","STSO","TT","UNSW","NCHG","JNUM","FID",
		"LID","TTX","CAM","WOCI","TRM","BTO","STR","EPW","ICN","SWMZ",
		"LL","MM","COHS","ARRT","BZT","CTAK","DC","JST","KMZT","KLT",
		"QCTE","SCT","SFR","AUM","PLD","POW","BDG","CNG","TYP","MTB",
		"NGY","STD","TTI","MNN","YAN","GG","ITGT","PID","MR","3LED",
		"EBD","PUID","LI","ACL","EH","TTN","CBR","R","H","RM",
		"RX","PWD","HEX","MUL","NTT","BRD","KYU","SSHS","SHT","7",
		"MRC","OMF","BMOR","BLX","DIC","KGB","MENM","YGOL","EID","IPA",
		"LOD","DACH","DW","JB","NDN","BYS","MCH","GNP","NV","AND",
		"HSH","RGBL","JXSQ","SHM","ANS","TML","WGC","PLI","SYH","ELET",
		"CDU","DU","IS","FG","UTTT","LYN","NOT","PZ","RGBS","DA",
		"ISS","RS","BLWS","DCD","RGBA","SD","FIF","RPST","DTA","PXCI",
		"TGV","21","NGT","PRT","CALC","ASMZ","SIX","ULO","BUSY","SPGS",
		"ASCD","CMCH","SUS","BDS","ZHS","CNU","NCB","CCS","PA","RVE",
		"FACL","0","BKBI","CTM","CBI","FBI","II","PFP","CCB","DCR",
		"RGBF","TMW","TAMS","AFC","EN4","KE","42","501","DCI","CLOT",
		"BRK","BNO","SBZ","TMOD","ZHZ","MMZE","DDG","FLI","NKN","OLTA",
		"TNDY","WT","RUS","TP","DBL","UCR","UFR","WGOL","GF","MIL",
		"LIT","RGX","CESH","COBU","MEC","PNTA","BT","DGS","KG","MZY",
		"SIEX","OWO","TCS","SPA","TSP","9B","MMM","MNM","CNS","PPLA",
		"BRDG","BNA","BNS","CBNS","JBNS","LBNS","RBNS","BKR","BTF","DI",
		"CN5","SISM","TARR","FIND","KC","16C","ABT","SBA","DGG","DP",
		"CRY","HH","RMCH","RMCH","RMCH","AGID","INU","QTE","TBS","XNB",
		"GWA","SA","DGR","EOO","HOL","LOOP","MMR","RRR","WOUT","CLB",
		"GNFY","NNN","CHEX","LON","CMT","LAS","SYQ","CU","PART","CRST",
		"TLP","BM","FAC","NOM","TATU","MMSG","TM","CCTH","SSK","CM",
		"MLC","SMB","MUTB","SSM","TRC","OR","FCHC","PTS","BYW","OMD",
		"ATCI","GBW","NUMB","VD","VPKR","BMG","JS","WARR","PGRA","KPD",
		"TPB","LLY","TWS","TEP","RGBR","SNA","SC","JTV","MTR","SBM",
		"RJBX","TCB","ADD","BMC","SMPD","UD","Q","WOSC","AKM","TNKF",
		"RTM","SDC","BDW","FLRL","VA","EAS","DIO","ZC","RL","STK",
		"TRNT","KARR","CARR","CRL","FARR","DSR","FMZN","TSSP","BPS","TSCP",
		"PDC","MSA","PHO","KX","VK","NKM","BDA","RBA","SCBS","HIO",
		"SSUB","AKP","BCKB","CPS","HXM","BNSS","BBN","CHB","FBNS","KBNS",
		"SMMZ","BL","QLSH","TMWY","DNAM","EN1","LINQ","SPS","BTU","RGBH",
		"5A","RSU","DIW","PO","TDOC","XM","YM","RMOS","XE","CPXT",
		"STSQ","SMC","FRMZ","STTB","WSI","FILE","OCT","CLLT","BCFL","FUS",
		"KA","81","MII","UDR","SWD","NIL","FMA","KM","STSG","VLTG",
		"ASTR","CORR","XC","CSP","DY","FPW","LFPW","LPW","NTM","BRNT",
		"ACDS","CSN","NCI","CCR","IDNT","OL","DBKB","IP","SPK","NMC",
		"123G","HOO","KPM","PPZ","DFN","HBNS","YBNS","SKR","GMM","LTGR",
		"NWL","+","AMP","RSCI","SSVR","TMTP","IRVL","SOME","ORB","SCI",
		"MHMR","HTR","LAD","CPU","DEC","CT69","MSVL","COIN","EMTM","NRBT",
		"NC","N","1DCH","IDFR","SSP","123","CFX","NCF","NCC","NCO",
		"NCT","NMU","NMM","FGMZ","NPH","LPJB","BIB","ALT","ASAR","NWRS",
		"PF","TF","LLA","NYA","CSYH","VF","DMFR","PGR","SUS2","MSM",
		"CF","DIRB","MAC","A","CLMX","AMD","BLWH","BMG2","DMB","HELO",
		"IMP","UNCB","WPDY","DCCI","SOUL","WKDS","FRCG","INFL","MZA","SLT",
		"ALBV","HIT","STOI","CLO","DLT","HTT","SPTR","CART","CSZ","8",
		"FSN","NDC","KWL","STS","RH","SAD","BSH","RNHL","RN","MET",
		"PM","SQZ","LFA","MZID","PKAR","AGPH","PWN","SUD","SIMP","ENM",
		"DK","FD","PZID","FACE","IKP","SISH","BGG","MALD","DIE","CACO",
		"SSHO","MQM","LEQ","SMR","WH","2048","PLM","MSE","STB","CPC",
		"LPP","MMRC","WRN","WQB","CID","OOT","CKYS","MME","MIR","PNS",
		"SCF","SKE","AR","WRDS","IIL","PL","ARP","PER","COV","RGBC",
		"WA","TIK","GUNK","FSW","10A","TET","NBPZ","SISA","MSEQ","SQLB",
		"MWIS","TTM","TELE","CDT","KUSA","SQLE","L","SQLC","QUIZ","SUP",
		"CLTR","DRTR","FLTR","SKTR","SLTR","TTR","TRTR","DCF","GO","NEM",
		"NSYC","NBU","NWOS","NX","NSPT","UCF","BZS","SGI","TTZ","NUL",
		"SHK","HOR","LOCH","SHTB","HCLR","CAN","CK","CCAN","LP","PCP",
		"RIP","SM","SRYS","TER","TBW","AQ","CA","MLDM","ETP","INS",
		"NP","SBID","4DMZ","BITA","WOM","ATS","MMTC","VAR","EXM","CRMZ",
		"GEM","NXR","WHEN","RPK","SPMZ","CAKE","NTS","PS","SCSC","BS1",
		"DV","GYAR","CONS","VADD","CRIT","MZSK","SSAT","VM","DUKO","IKEA",
		"GOC","C","BLB","MP","SPP","CRKP","INC","SL","ZZ","HP",
		"MTMS","PIRA","SSG","SSC","UNO!","OKL","TTB","TTL","TTF","3N",
		"MOM","TOT","XO","OP","SGS","BCCI","MAY","OGS","BIM","SPRT",
		"CMOD","EBVD","FSQ","SSTK","CHRG","MPK","MASK","FIS","GNCI","IVS",
		"LTM","DDI","KBTN","BBTN","CBTN","ABTN","GLBTN","GBTN","OBTN","IBTN",
		"RBTN","PBTN","TBTN","WBTN","YBTN","SSHF","DOT","FAUB","NIX","DSUM",
		"IDC","OMNI","TWI","FOV","BHC","YHC","EBO","LZC","NKJ","SONG",
		"SHCI","BNT","TPTS","MCI","PE","UND","RBNT","TASQ","CHS","QLT",
		"BALN","FMSN","MTH","MON","IMB","SURB","CCI","SWP","BBX","FCD",
		"MTRK","TMA","RO3","RO","GMZ","IDE","CLP","MCPL","PZP","FGM",
		"NNP","NPK","NPR","NTXF","ASQ","BMT","T","FCI","LAW","TRJ",
		"ABY","W2TG","UT","FLP","SMBH","ALG","SMS","ABUT","NBTN","DBD",
		"HIPS","SILA","BAK","PRMZ","CFBTN","BE","SSFT","BKC","SHF","UPSQ",
		"3DTC","CHM","EGC","OCMN","MSF","NTPB","BDB","BSG","XRO","FAO",
		"MTCR","PRB","USC","HTK","NCS","SNM","NE","RLGL","FFSD","MIND",
		"4BT","WHKP","HGL","DOOF","SBE","SYMB","ZM","ADN","DRA","HM",
		"RCF","VCF","NOTE","SIA","SHIN","JCF","TFA","FCF","MA","E",
		"XRI","FACU","SESA","SWI","ENL","CRMC","FAE","LEVD","TMWH","XRD",
		"MGCI","OWCI","SSHT","TRP","SMK","COCI","GFRG","KNW","LAM","CMCI",
		"MGSQ","PFL","MGC","G","ATL","QSPT","QST","MUBT","PHQB","UACN",
		"FCR","BST","TDOR","WRDL","DBMZ","TRVI","KBT","PSC","BKS","BLC",
		"LGPL","NBKS","MVD","RMSP","SPDR","JNJ","SMT","MTSN","TYR","EA",
		"TSC","ZLP","RMTS","SIST","TEM","TGP","AGN","MXM","NBM","NCSW",
		"NDO","NPP","NSP","MZEM","LOLB","CAS","POL","LHQ","MNSQ","TWG",
		"INV","UIN","PKBO","BOW","DSEQ","WVCL","PPW","PTY","SHTS","AMEM",
		"DON","FMM","OIOM","SBN","LEAN","PGF","TMOT","CHSF","RGLG","WI",
		"NFN","MJQH","MJQE","MJQS","MINI","DSCI","MTL","HTZ","BZG","EGF",
		"NOID","WU","BSHD","FIT","PTRS","SDM","MEME","ARC","CLRG","FIL",
		"SPDT","SHFW","MDL","MID","SPOS","UCK","TMZ","HF","GMCH","LKW",
		"BNM","PCR","RGBQ","PLAG","BWB","GDV","TES","TB","DOM","CWOS",
		"WAN","TRCT","EWOS","CIM","TMS","TGW","TKP","TTRM","TPZ","WOFA",
		"CCO","PIPN","DL","RBTL","WHOF","BOI","QTC","XY","CPIP","SGB",
		"MCDS","SSWM","CRT","FLC","TTP","FRCI","T10","MMCR","MMNV","BRCP",
		"SRSQ","SSHD","HOLM","UN","MWD","MSHD","SSN","CHW","K","WHI",
		"QSWR","BTT","CTF","MLMY","MBZ","TWT","WRT","FTIC","ENAC","IRLU",
		"TKS","FFR","SSE","OBBT"
	};
	void Awake()
	{
		moduleID = moduleCounterID++;
		flag = false;
		nameList = nameList.Shuffle();
		for (int i = 0; i < 4; i++)
			cryptoScreens[i].text = nameList[i];
		List<int> poss = new List<int>();
		for (int i = 1; i < 10000; i++)
			poss.Add(i);
		poss = poss.Shuffle();
		int[][] quarterPrices = new int[4][];
		int[][] quarterChanges = new int[4][];
		Q4Prices = new int[4];
		QChanges = new string[4][];
		QColors = new Color[4][];
		for (int i = 0; i < 4; i++)
		{
			quarterPrices[i] = new int[5];
			quarterChanges[i] = new int[4];
			QChanges[i] = new string[4];
			QColors[i] = new Color[4];
			for (int j = 0; j < 5; j++)
				quarterPrices[i][j] = poss[(i * 5) + j];
			for (int j = 1; j < 5; j++)
			{
				quarterChanges[i][j - 1] = quarterPrices[i][j] - quarterPrices[i][j - 1];
				if(quarterChanges[i][j - 1] < 0)
				{
					QChanges[i][j - 1] = (-quarterChanges[i][j - 1]) + "▾";
					QColors[i][j - 1] = Color.red;
				}
				else
				{
					QChanges[i][j - 1] = quarterChanges[i][j - 1] + "▴";
					QColors[i][j - 1] = Color.green;
				}
			}
			Q4Prices[i] = quarterPrices[i][4] + 0;
			screens[i].text = Q4Prices[i] + "";
			Debug.LogFormat("[Crypto Market #{0}] Crypto #{1} Prices: {2} {3} {4} {5} {6}", moduleID, (i + 1), quarterPrices[i][0], quarterPrices[i][1], quarterPrices[i][2], quarterPrices[i][3], quarterPrices[i][4]);
			Debug.LogFormat("[Crypto Market #{0}] Crypto #{1} Changes: {2} {3} {4} {5}", moduleID, (i + 1), quarterChanges[i][0], quarterChanges[i][1], quarterChanges[i][2], quarterChanges[i][3]);
		}
		int[] peaks = new int[4], slumps = new int[4], vol = new int[4];
		for(int i = 0; i < 4; i++)
		{
			peaks[i] = quarterPrices[i][1];
			slumps[i] = quarterPrices[i][1];
			for (int j = 2; j < 5; j++)
			{
				if (peaks[i] < quarterPrices[i][j])
					peaks[i] = quarterPrices[i][j];
				if (slumps[i] > quarterPrices[i][j])
					slumps[i] = quarterPrices[i][j];
			}
			Debug.LogFormat("[Crypto Market #{0}] Crypto #{1} Peak: {2}", moduleID, (i + 1), peaks[i]);
			Debug.LogFormat("[Crypto Market #{0}] Crypto #{1} Slump: {2}", moduleID, (i + 1), slumps[i]);
			int[] flux = new int[4];
			for (int j = 0; j < 4; j++)
				flux[j] = (quarterChanges[i][j] * 100) / quarterPrices[i][j];
			Debug.LogFormat("[Crypto Market #{0}] Crypto #{1} Fluctuations: {2}% {3}% {4}% {5}%", moduleID, (i + 1), flux[0], flux[1], flux[2], flux[3]);
			Array.Sort(flux);
			vol[i] = flux[3] - flux[0];
			Debug.LogFormat("[Crypto Market #{0}] Crypto #{1} Volatile Score: {2}", moduleID, (i + 1), vol[i]);
		}
		int[][] order =	{getOrder(peaks),getOrder(slumps)};
		int[] scoreOrder = { 3, 6, 9, 12 };
		int[] scores = { 0, 0, 0, 0 };
		for(int i = 0; i < 2; i++)
		{
			for(int j = 0; j < 4; j++)
				scores[order[i][j]] += scoreOrder[j];
			//Debug.LogFormat("[Crypto Market #{0}] Order: {1} {2} {3} {4}", moduleID, order[i][0], order[i][1], order[i][2], order[i][3]);
		}
		//vol = new int[] { 6, 5, 5, 5 };
		int[] temp = new int[vol.Length];
		for (int i = 0; i < temp.Length; i++)
			temp[i] = vol[i] + 0;
		Array.Sort(temp);
		if(temp[0] == temp[1])
		{
			if(temp[0] == temp[2])
			{
				if(temp[0] == temp[3])
					scoreOrder = new int[]{ 10 };
				else
					scoreOrder = new int[] { 12, 4 };
			}
			else if(temp[2] == temp[3])
				scoreOrder = new int[] { 14, 6 };
			else
				scoreOrder = new int[] { 14, 8, 4 };
		}
		else if(temp[1] == temp[2])
		{
			if(temp[1] == temp[3])
				scoreOrder = new int[] { 16, 8 };
			else
				scoreOrder = new int[] { 16, 10, 4 };
		}
		else if(temp[2] == temp[3])
			scoreOrder = new int[] { 16, 12, 6 };
		else
			scoreOrder = new int[] { 16, 12, 8, 4 };
		temp = temp.Distinct().ToArray();
		for(int i = 0; i < temp.Length; i++)
		{
			for(int j = 0; j < vol.Length; j++)
			{
				if (temp[i] == vol[j])
					scores[j] += scoreOrder[i];
			}
		}

		Debug.LogFormat("[Crypto Market #{0}] Scores: {1} {2} {3} {4}", moduleID, scores[0], scores[1], scores[2], scores[3]);
		int numStocks = UnityEngine.Random.Range(0, 16) + 20;
		ans = new int[4];
		int money = 0;
		for (int i = 0; i < 4; i++)
		{
			ans[i] = (numStocks * scores[i]) / 100;
			money += (ans[i] * Q4Prices[i]);
		}
		Debug.LogFormat("[Crypto Market #{0}] Number of Crypto: {1} {2} {3} {4}", moduleID, ans[0], ans[1], ans[2], ans[3]);
		Debug.LogFormat("[Crypto Market #{0}] Money Required: {1}", moduleID, money);
		moneyScreen.text = money + "";

		int[] indexes = { 0, 1, 2, 3 };
		foreach (int index in indexes)
			cryptos[index].OnInteract = delegate { pressCrypto(index); return false; };
		view.OnInteract = delegate { pressView(); return false; };
		invest.OnInteract = delegate { pressInvest(); return false; };
		foreach (int index in indexes)
			UP[index].OnInteract = delegate { pressArrow(index, 1); return false; };
		foreach (int index in indexes)
			DOWN[index].OnInteract = delegate { pressArrow(index, -1); return false; };
	}
	private int[] getOrder(int[] arr)
	{
		int[] temp = new int[arr.Length];
		for (int i = 0; i < temp.Length; i++)
			temp[i] = arr[i] + 0;
		Array.Sort(temp);
		int[] order = new int[4];
		for (int i = 0; i < temp.Length; i++)
		{
			for(int j = 0; j < arr.Length; j++)
			{
				if(temp[i] == arr[j])
				{
					order[i] = j;
					break;
				}
			}
		}
		return order;
	}
	private void pressCrypto(int index)
	{
		audio.PlaySoundAtTransform(SFX[0].name, transform);
		if (flag)
			outOfSub();
		for (int i = 0; i < 4; i++)
		{
			screens[i].text = QChanges[index][i];
			screens[i].color = QColors[index][i];
			cryptoScreens[i].color = Color.white;
		}
		cryptoScreens[index].color = Color.magenta;
	}
	private void pressView()
	{
		audio.PlaySoundAtTransform(SFX[0].name, transform);
		if (flag)
			outOfSub();
		for (int i = 0; i < 4; i++)
		{
			screens[i].text = Q4Prices[i] + "";
			screens[i].color = Color.cyan;
			cryptoScreens[i].color = Color.white;
		}
	}
	private void pressInvest()
	{
		float[] pos = { 0.0f, -0.025f, -0.05f, -0.075f };
		if (flag)
		{
			if(sub[0] == ans[0] && sub[1] == ans[1] && sub[2] == ans[2] && sub[3] == ans[3])
			{
				int[] indexes = { 0, 1, 2, 3 };
				invest.OnInteract = null;
				view.OnInteract = null;
				foreach(int index in indexes)
				{
					cryptos[index].OnInteract = null;
					UP[index].OnInteract = null;
					DOWN[index].OnInteract = null;
				}
				moneyScreen.text = "GOOD WORK";
				audio.PlaySoundAtTransform(SFX[1].name, transform);
				module.HandlePass();
			}
			else
				module.HandleStrike();
		}
		else
		{
			audio.PlaySoundAtTransform(SFX[0].name, transform);
			flag = true;
			for(int i = 0; i < 4; i++)
			{
				sub[i] = 0;
				screens[i].text = "0";
				screens[i].color = Color.yellow;
				cryptoScreens[i].color = Color.white;
			}
			for (int i = 0; i < 4; i++)
				arrows[i].localPosition = new Vector3(0.0f, 0.0f, pos[i]);
		}
	}
	private void pressArrow(int i, int dir)
	{
		audio.PlaySoundAtTransform(SFX[0].name, transform);
		sub[i] += dir;
		if (sub[i] < 0)
			sub[i] = 99;
		if (sub[i] > 99)
			sub[i] = 0;
		screens[i].text = sub[i] + "";
	}
	private void outOfSub()
	{
		flag = false;
		for (int i = 0; i < 4; i++)
			arrows[i].localPosition = new Vector3(0.0f, -0.01f, 0.0f);
	}
#pragma warning disable 414
	private readonly string TwitchHelpMessage = @"!{0} press|p view/v 1 2 3 4 presses the view button, Crypto #1, Crypto #2, Crypto #3, and Crypto #4 in that order. !{0} invest # # # # invests those values to each Crypto in reading order.";
#pragma warning restore 414
	IEnumerator ProcessTwitchCommand(string command)
	{
		string[] param = command.ToUpper().Split(' ');
		if ((Regex.IsMatch(param[0], @"^\s*PRESS\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(param[0], @"^\s*P\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant)) && param.Length > 1)
		{
			if (isPos(param))
			{
				yield return null;
				for (int i = 1; i < param.Length; i++)
				{
					switch (param[i])
					{
						case "V":
						case "VIEW":
							view.OnInteract();
							break;
						default:
							cryptos[param[i][0] - '1'].OnInteract();
							break;
					}
					yield return new WaitForSeconds(1.0f);
				}
			}
			else
				yield return "sendtochat An error occured because the user inputted something wrong.";
		}
		else if (Regex.IsMatch(param[0], @"^\s*INVEST\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) && param.Length == 5)
		{
			if(isNum(param))
			{
				yield return null;
				if(!flag)
					invest.OnInteract();
				for(int i = 1; i < param.Length; i++)
				{
					var val = int.Parse(param[i]);
					int[] diff = { mod(val - sub[i - 1], 100), mod(sub[i - 1] - val, 100) };
					while(sub[i - 1] != val)
					{
						yield return new WaitForSeconds(0.1f);
						if (diff[0] < diff[1])
							UP[i - 1].OnInteract();
						else
							DOWN[i - 1].OnInteract();
					}
				}
				yield return new WaitForSeconds(0.1f);
				invest.OnInteract();
			}
			else
				yield return "sendtochat An error occured because the user inputted something wrong.";
		}
		else
			yield return "sendtochat An error occured because the user inputted something wrong.";
	}
	IEnumerator TwitchHandleForcedSolve()
	{
		if (!(flag))
			invest.OnInteract();
		for (int i = 0; i < 4; i++)
		{
			int[] diff = { mod(ans[i] - sub[i], 100), mod(sub[i] - ans[i], 100) };
			while (sub[i] != ans[i])
			{
				if (diff[0] < diff[1])
					UP[i].OnInteract();
				else
					DOWN[i].OnInteract();
				yield return new WaitForSeconds(0.1f);
			}
		}
		invest.OnInteract();
		yield return new WaitForSeconds(0.1f);
	}
	private bool isPos(string[] p)
	{
		for(int i = 1; i < p.Length; i++)
		{
			switch (p[i])
			{
				case "VIEW":
				case "V":
				case "1":
				case "2":
				case "3":
				case "4":
					break;
				default:
					return false;
			}
		}
		return true;
	}
	private bool isNum(string[] p)
	{
		int test = 0;
		for(int i = 1; i < p.Length; i++)
		{
			if (int.TryParse(p[i], out test))
			{
				if (test < 0 || test > 99)
					return false;
			}
			else
				return false;
		}
		return true;
	}
	private int mod(int n, int m)
	{
		while (n < 0)
			n += m;
		return (n % m);
	}
}
