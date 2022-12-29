use chrono::{TimeZone, Datelike};
use substring::Substring;

/*
 * Zenith 4.0.3
 * by Jonathan Heitz
 * 2017-2022
 * 2022-2022 now in  R u s t
 */

/*
 * TO-DO
 * -Latency
 */

const ME:			&str	= "<@284056426760896522>";
const JHEITZ_ID:	u64		= 284056426760896522;
const ZENITH_ID:	u64		= 387021961919987722;
const JOE_ID:		u64		= 357985448892563457;
const SICK_SEVEN:	u64		= 701528955358019724;
const ODD_SQUAD:	u64		= 357905269759672332;
const TRIP_PLANS:	u64		= 781012433585963028;
const SUGGESTIONS:	u64		= 914054832753639455;
const SUGGESTIONS2:	u64		= 923787496817569792;

static mut BDAY:	bool	= false;

struct Bot;

#[serenity::async_trait]
impl serenity::client::EventHandler for Bot {

	async fn message(&self, ctx: serenity::client::Context, msg: serenity::model::channel::Message) {

		let msg_lower = msg.content.to_lowercase();
		let mention = format!("<@{}>", msg.author.id.as_u64());
		let now = chrono::Local::now();

		unsafe {

			if now.month() == 12 && now.day() == 3 {
				BDAY = true;
				ctx.set_activity(serenity::model::prelude::Activity::playing("on my birthday!")).await;
			} else if BDAY == true {
				BDAY = false;
				ctx.set_activity(serenity::model::prelude::Activity::playing("Pokémon Crystal")).await;
			}

		}

		if msg.author.id != ZENITH_ID {

			if rand::random::<u32>() / (u32::MAX / 8192) == 69 {

				let _ = msg.channel_id.say(
					&ctx.http, 
					format!("{}, your message was shiny!", mention)
				).await;

				let _ = msg.react(&ctx.http, '⭐').await;

			}

			if msg.content.contains(&ZENITH_ID.to_string()) {

				let _ = msg.channel_id.say(
					&ctx.http, 
					"Awaiting command! :upside_down:"
				).await;

			}

			if msg_lower.starts_with("zenith, ping") {

				let _ = msg.reply(
					&ctx.http, 
					"This feature is not working properly at the moment. It is being looked into."
				).await;

			}

			else if msg_lower.starts_with("zenith, say: ") {

				if msg.author.id == JHEITZ_ID {

					let _ = msg.channel_id.say(
						&ctx.http, 
						msg.content.substring(13, msg.content.chars().count())
					).await;

				} else {

					let _ = msg.channel_id.say(
						&ctx.http, 
						format!(
							"Sorry, but only {} can make me say stuff.\n",
							ME
						)
					).await;

					let _ = msg.channel_id.say(
						&ctx.http, 
						"You can use `Zenith, spam: <message>` if you like."
					).await;

				}

			}

			else if msg_lower.starts_with("zenith, ") && (msg_lower.contains("pokémon") || msg_lower.contains("pokemon")) {

				let random: u32 = rand::random::<u32>() / (u32::MAX / 721);
				let path: &str = &format!("{}{}{}", "/home/jheitz/zenith/pokemon/", random, ".png");
				let paths = vec![path];
				let _ = msg.channel_id.send_files(&ctx.http, paths, |m| m.content("")).await;
				
				let _ = msg.channel_id.say(
					&ctx.http, 
					"Here you go!"
				).await;

			}

			else if msg_lower.starts_with("zenith, spam: ") {

				if msg.author.id != JOE_ID {

					let spam = msg.content.substring(14, msg.content.chars().count());
					let _ = msg.channel_id.say(
						&ctx.http, 
						format!("Okay, I will spam \"{}\" for you.", spam)
					).await;

					std::thread::sleep(std::time::Duration::from_secs(1));

					for _ in 0..20 {

						let _ = msg.channel_id.say(
							&ctx.http, 
							spam
						).await;
						std::thread::sleep(std::time::Duration::from_secs(1));

					}

					let _ = msg.channel_id.say(
						&ctx.http, 
						"Spammed your message 20 times."
					).await;

				} else {

					let _ = msg.channel_id.say(
						&ctx.http, 
						"You don't have permission to use that command."
					).await;

				}

			} else if msg_lower.starts_with("im ") {

				let _ = msg.channel_id.say(
					&ctx.http, 
					format!("Hello, {}, I'm Zenith!", msg.content.substring(2, msg.content.chars().count()))
				).await;

			} else if msg_lower.starts_with("i'm ") {

				let _ = msg.channel_id.say(
					&ctx.http, 
					format!("Hello, {}, I'm Zenith!", msg.content.substring(3, msg.content.chars().count()))
				).await;

			} else if msg_lower.starts_with("i am ") {

				let _ = msg.channel_id.say(
					&ctx.http, 
					format!("Hello, {}, I'm Zenith!", msg.content.substring(4, msg.content.chars().count()))
				).await;

			}

			else if msg_lower.starts_with("zenith, ") && msg_lower.contains("draft") {

				let now = chrono::Local::now();
				let nfl_draft_start = chrono::Local.with_ymd_and_hms(2023, 4, 27, 0, 0, 0).unwrap();
				let nfl_draft_end = chrono::Local.with_ymd_and_hms(2023, 4, 29, 23, 59, 59).unwrap();

				if now < nfl_draft_start {

					let days = nfl_draft_start.signed_duration_since(now).num_days();

					let _ = msg.channel_id.say(
						&ctx.http, 
						format!(
							"There are {} day(s) until the 2023 NFL draft. It begins on April 27, 2023.",
							days
						)
					).await;

				} else if now > nfl_draft_end {

					let _ = msg.channel_id.say(
						&ctx.http, 
						"The 2023 NFL Draft already happened."
					).await;

				} else {

					let _ = msg.channel_id.say(
						&ctx.http, 
						"The 2023 NFL Draft is happening today."
					).await;

				}

			}

			if msg.guild_id.unwrap() == SICK_SEVEN || 
				msg.guild_id.unwrap() == ODD_SQUAD ||
				msg.guild_id.unwrap() == TRIP_PLANS || 
				msg.guild_id.unwrap() == SUGGESTIONS || 
				msg.guild_id.unwrap() == SUGGESTIONS2  {
				
				if msg_lower.contains("cowboys") {
					let _ = msg.channel_id.say(
						&ctx.http, 
						"WE DEM BOYZ :triumph:"
					).await;
				} 
				
				if msg_lower.contains("dak ") || msg_lower.contains("prescot") {
					let _ = msg.channel_id.say(
						&ctx.http, 
						"DAK ATTACK :fist:"
					).await;
				} 
				
				if msg_lower.contains("zeke ") || msg_lower.contains("ezekiel") || msg_lower.contains("elliot") {
					let _ = msg.channel_id.say(
						&ctx.http, 
						"FEED ZEKE :cut_of_meat:"
					).await;
				}

				if msg_lower.contains("primetime") || msg_lower.contains("prime time") {
					let _ = msg.channel_id.say(
						&ctx.http, 
						"WE STILL PRIME TIME :clock3:"
					).await;
				}

				if msg_lower.contains("america") {
					let _ = msg.channel_id.say(
						&ctx.http, 
						"WE STILL AMERICA'S TEAM :flag_us:"
					).await;
				}

				if msg_lower.contains("oakland") {
					let _ = msg.channel_id.say(
						&ctx.http, 
						"Las Vegas*"
					).await;
				}

				if msg_lower.contains("redskins") {
					let _ = msg.channel_id.say(
						&ctx.http, 
						"Commanders*"
					).await;
				}

				if msg_lower.contains("san diego") {
					let _ = msg.channel_id.say(
						&ctx.http, 
						"Los Angeles*"
					).await;
				}

				if msg_lower.contains("buccs") {
					let _ = msg.channel_id.say(
						&ctx.http, 
						"Bucs*"
					).await;
				}

			}

		}

	}

	async fn ready(&self, ctx: serenity::client::Context, _: serenity::model::prelude::Ready) {

		let now = chrono::Local::now();
		if now.month() == 12 && now.day() == 3 {
			unsafe {
				BDAY = true;
				ctx.set_activity(serenity::model::prelude::Activity::playing("on my birthday!")).await;
			}
		} else {
			ctx.set_activity(serenity::model::prelude::Activity::playing("Pokémon Crystal")).await;
		}

		println!("Zenith is now ONLINE");

	}

}

#[tokio::main]
async fn main() {

	println!("Starting Zenith {}", env!("CARGO_PKG_VERSION"));
	let mut client = serenity::Client::builder(
		std::env::var("DISCORD_TOKEN").expect("Token error"),
		serenity::prelude::GatewayIntents::GUILD_MESSAGES | serenity::prelude::GatewayIntents::MESSAGE_CONTENT
	).event_handler(Bot)
		.await
		.expect("Error starting client :(");
	let _ = client.start().await;

}